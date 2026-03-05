using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.Financial.Domain.Entities;
using EnterpriseMediator.Financial.Domain.Interfaces;
using EnterpriseMediator.Financial.Infrastructure.Persistence.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace EnterpriseMediator.Financial.Infrastructure.Gateways.Stripe
{
    /// <summary>
    /// Infrastructure adapter for the Stripe Payment Gateway.
    /// Encapsulates all interactions with the Stripe.net SDK to ensure the domain remains decoupled from specific payment provider implementations.
    /// Implements creation of payment links/sessions and handles idempotency via metadata.
    /// </summary>
    public class StripePaymentAdapter : IPaymentGateway
    {
        private readonly StripeSettings _settings;
        private readonly ILogger<StripePaymentAdapter> _logger;
        private readonly IStripeClient _stripeClient;

        public StripePaymentAdapter(
            IOptions<StripeSettings> settings,
            ILogger<StripePaymentAdapter> logger)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (string.IsNullOrWhiteSpace(_settings.ApiKey))
            {
                throw new InvalidOperationException("Stripe API Key is not configured.");
            }

            // Initialize the Stripe Client with the API Key from settings.
            // This allows for thread-safe usage and easier testing compared to static configuration.
            _stripeClient = new StripeClient(_settings.ApiKey);
        }

        /// <summary>
        /// Creates a Stripe Checkout Session for the provided invoice.
        /// </summary>
        /// <param name="invoice">The domain invoice entity containing amount, currency, and references.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A result containing the Payment ID (Session ID) and the public URL for the client.</returns>
        /// <exception cref="ArgumentNullException">Thrown if invoice is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if invoice amount or currency is invalid.</exception>
        public async Task<PaymentLinkResult> CreatePaymentLinkAsync(Invoice invoice, CancellationToken cancellationToken)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));

            _logger.LogInformation("Initiating Stripe Checkout Session creation for Invoice {InvoiceId} in Project {ProjectId}.", invoice.Id, invoice.ProjectId);

            try
            {
                ValidateInvoiceForPayment(invoice);

                // Convert decimal amount to smallest currency unit (e.g., cents for USD/EUR).
                // Note: comprehensive implementation would handle zero-decimal currencies (JPY) dynamically.
                // Assuming 2-decimal standard for this implementation context.
                long amountInSmallestUnit = (long)(invoice.TotalAmount.Amount * 100);

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = amountInSmallestUnit,
                                Currency = invoice.TotalAmount.Currency.Code.ToLowerInvariant(),
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = $"Invoice #{invoice.Id}",
                                    Description = $"Payment for Project {invoice.ProjectId}",
                                    Metadata = new Dictionary<string, string>
                                    {
                                        { "ProjectId", invoice.ProjectId.ToString() },
                                        { "InvoiceId", invoice.Id.ToString() }
                                    }
                                },
                            },
                            Quantity = 1,
                        },
                    },
                    Mode = "payment",
                    // Metadata on the Session object itself for easier webhook reconciliation
                    Metadata = new Dictionary<string, string>
                    {
                        { "ProjectId", invoice.ProjectId.ToString() },
                        { "InvoiceId", invoice.Id.ToString() },
                        { "ClientId", invoice.ClientId.ToString() }
                    },
                    // Payment Intent Data allows us to set metadata on the underlying PaymentIntent that is created
                    PaymentIntentData = new SessionPaymentIntentDataOptions
                    {
                        Metadata = new Dictionary<string, string>
                        {
                            { "InvoiceId", invoice.Id.ToString() },
                            { "ProjectId", invoice.ProjectId.ToString() }
                        },
                        CaptureMethod = "automatic"
                    },
                    // URLs should ideally come from configuration, using placeholders here for the adapter logic
                    SuccessUrl = $"{_settings.ClientBaseUrl}/invoices/{invoice.Id}/success?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"{_settings.ClientBaseUrl}/invoices/{invoice.Id}/cancel",
                    ClientReferenceId = invoice.Id.ToString() // Useful for reconciliation
                };

                // Idempotency Key ensures we don't create multiple sessions for the same invoice attempt if retried.
                // We use the InvoiceId combined with a status or timestamp if multiple attempts are allowed, 
                // but for a unique invoice payment link, the Invoice ID serves as a strong base.
                var requestOptions = new RequestOptions
                {
                    IdempotencyKey = $"invoice_checkout_{invoice.Id}"
                };

                var service = new SessionService(_stripeClient);
                Session session = await service.CreateAsync(options, requestOptions, cancellationToken);

                _logger.LogInformation("Stripe Checkout Session created successfully. SessionId: {SessionId}, PaymentIntentId: {PaymentIntentId}", session.Id, session.PaymentIntentId);

                // Note: PaymentIntentId might be null in the response of CreateAsync for 'payment' mode 
                // until the user interacts, but Session ID is the primary reference for Checkout.
                return new PaymentLinkResult
                {
                    PaymentId = session.Id, // Storing Session ID as the primary external reference initially
                    Url = session.Url,
                    ExternalReference = session.PaymentIntentId // May be populated if available
                };
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe API Error creating payment link for Invoice {InvoiceId}. Type: {ErrorType}, Code: {ErrorCode}", invoice.Id, ex.StripeError?.Type, ex.StripeError?.Code);
                // Throwing a domain-agnostic exception or custom infrastructure exception would be ideal here
                // depending on the global exception handling strategy. 
                throw new InvalidOperationException($"Failed to create payment link via Stripe: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating Stripe payment link for Invoice {InvoiceId}.", invoice.Id);
                throw;
            }
        }

        private void ValidateInvoiceForPayment(Invoice invoice)
        {
            if (invoice.TotalAmount == null)
            {
                throw new InvalidOperationException($"Invoice {invoice.Id} has no TotalAmount defined.");
            }

            if (invoice.TotalAmount.Amount <= 0)
            {
                throw new InvalidOperationException($"Invoice {invoice.Id} amount must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(invoice.TotalAmount.Currency?.Code))
            {
                throw new InvalidOperationException($"Invoice {invoice.Id} has an invalid currency code.");
            }
        }
    }
}