using EnterpriseMediator.Financial.Domain.Entities;
using EnterpriseMediator.Financial.Domain.Enums;
using EnterpriseMediator.Financial.Domain.Interfaces;
using EnterpriseMediator.Financial.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseMediator.Financial.Application.Features.Payments.EventHandlers
{
    /// <summary>
    /// Command to process a successful payment event received from Stripe webhooks.
    /// This is typically dispatched by the Web API Controller after signature verification.
    /// </summary>
    public record ProcessStripePaymentCommand(
        string PaymentIntentId, 
        decimal AmountReceived, 
        string CurrencyCode, 
        DateTimeOffset PaymentDate,
        string ExternalReferenceId) : IRequest<Result>;

    /// <summary>
    /// Handles the reconciliation logic when a payment success webhook is received.
    /// Updates Invoice status and creates Ledger Transactions.
    /// </summary>
    public class StripeWebhookHandler : IRequestHandler<ProcessStripePaymentCommand, Result>
    {
        private readonly IFinancialRepository _repository;
        private readonly ILogger<StripeWebhookHandler> _logger;

        public StripeWebhookHandler(
            IFinancialRepository repository,
            ILogger<StripeWebhookHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result> Handle(ProcessStripePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Processing Stripe payment webhook for Intent {PaymentIntentId}", request.PaymentIntentId);

                // 1. Retrieve the Invoice
                var invoice = await _repository.GetInvoiceByPaymentIntentIdAsync(request.PaymentIntentId, cancellationToken);

                if (invoice == null)
                {
                    // If we can't find the invoice, we can't process the payment.
                    // Returning Failure allows the Controller to decide whether to return 404/500 (triggering Stripe retry)
                    // or 200 (if we assume it's a data mismatch that retries won't fix).
                    // Usually, we want to log Critical and return failure to investigate.
                    _logger.LogCritical("Received payment for unknown Intent {PaymentIntentId}. Manual intervention required.", request.PaymentIntentId);
                    return Result.Failure($"Invoice not found for PaymentIntent {request.PaymentIntentId}");
                }

                // 2. Idempotency & State Check
                if (invoice.Status == InvoiceStatus.Paid)
                {
                    _logger.LogInformation("Invoice {InvoiceId} is already marked as PAID. Treating webhook as idempotent success.", invoice.Id);
                    return Result.Success();
                }

                // 3. Validate Amount
                // Financial integrity check: Did we get paid what we expected?
                // Note: Stripe amounts are often in cents, ensuring the Command has normalized this to decimal units is crucial.
                if (invoice.TotalAmount.Amount != request.AmountReceived)
                {
                    _logger.LogWarning("Payment mismatch for Invoice {InvoiceId}. Expected {Expected}, Received {Received}.", 
                        invoice.Id, invoice.TotalAmount.Amount, request.AmountReceived);
                    // Depending on business rule, we might partial pay or fail. 
                    // For this implementation, we accept it but log warning if it matches the payment intent ID.
                }

                // 4. Update Domain Entity
                // This method should contain the logic to set Status = Paid and raise InvoicePaidEvent
                invoice.MarkAsPaid(request.ExternalReferenceId, request.PaymentDate.DateTime);

                // 5. Create Ledger Transaction
                // Double-entry bookkeeping principle: Creating a Transaction record to represent the cash inflow.
                var transaction = new Transaction(
                    type: TransactionType.ClientPayment,
                    amount: new Domain.ValueObjects.Money(request.AmountReceived, new Domain.ValueObjects.Currency(request.CurrencyCode)),
                    timestamp: request.PaymentDate,
                    referenceId: invoice.Id, // Linking to the Invoice
                    externalReferenceId: request.ExternalReferenceId,
                    projectId: invoice.ProjectId,
                    clientId: invoice.ClientId
                );

                // 6. Persist Changes
                // The Repository.AddTransactionAsync and UpdateInvoice logic should run in the same Unit of Work
                await _repository.AddTransactionAsync(transaction, cancellationToken);
                
                // EF Core Change Tracking will handle the invoice update if it was retrieved from the context
                // But we call Update explicitly if the repo pattern requires it
                // await _repository.UpdateInvoiceAsync(invoice, cancellationToken); 

                await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully reconciled payment for Invoice {InvoiceId}. Transaction {TransactionId} created.", invoice.Id, transaction.Id);

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Stripe webhook for Intent {PaymentIntentId}", request.PaymentIntentId);
                // Return failure so the controller returns 500 and Stripe retries later
                return Result.Failure(ex.Message);
            }
        }
    }
}