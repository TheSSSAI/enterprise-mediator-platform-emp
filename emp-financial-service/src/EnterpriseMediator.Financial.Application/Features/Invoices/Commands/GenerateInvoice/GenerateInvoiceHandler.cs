using EnterpriseMediator.Financial.Domain.Entities;
using EnterpriseMediator.Financial.Domain.Interfaces;
using EnterpriseMediator.Financial.Domain.ValueObjects;
using EnterpriseMediator.Financial.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.Financial.Application.Common.Models; // Assuming Result<T> is here

namespace EnterpriseMediator.Financial.Application.Features.Invoices.Commands.GenerateInvoice
{
    public class GenerateInvoiceHandler : IRequestHandler<GenerateInvoiceCommand, Result<Guid>>
    {
        private readonly IFinancialRepository _repository;
        private readonly IPaymentGateway _paymentGateway;
        private readonly ILogger<GenerateInvoiceHandler> _logger;

        public GenerateInvoiceHandler(
            IFinancialRepository repository,
            IPaymentGateway paymentGateway,
            ILogger<GenerateInvoiceHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _paymentGateway = paymentGateway ?? throw new ArgumentNullException(nameof(paymentGateway));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<Guid>> Handle(GenerateInvoiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Initiating invoice generation for Project {ProjectId} Client {ClientId}", request.ProjectId, request.ClientId);

                // 1. Idempotency Check: Ensure an invoice doesn't already exist for this project
                // In many systems, a project might have multiple invoices, but for this specific scope 
                // we assume a 1:1 relationship for the "Initial Invoice".
                var existingInvoice = await _repository.GetInvoiceByProjectIdAsync(request.ProjectId, cancellationToken);
                if (existingInvoice != null)
                {
                    _logger.LogWarning("Attempted to generate duplicate invoice for Project {ProjectId}. Existing Invoice {InvoiceId}", request.ProjectId, existingInvoice.Id);
                    return Result<Guid>.Failure($"An invoice already exists for Project {request.ProjectId}");
                }

                // 2. Create Domain Entity
                // Validating money and currency support is handled within Money value object
                var money = new Money(request.Amount, new Currency(request.CurrencyCode));
                
                // Using a factory method or constructor on the entity
                var invoice = new Invoice(
                    request.ProjectId,
                    request.ClientId,
                    money
                );

                // 3. Integrate with Payment Gateway to generate Link/Intent
                // This is done BEFORE saving to ensure we have the external reference ID needed for webhooks
                _logger.LogInformation("Calling payment gateway to generate payment link for Invoice {InvoiceId}", invoice.Id);
                
                var gatewayResult = await _paymentGateway.CreatePaymentLinkAsync(invoice, cancellationToken);

                if (gatewayResult == null || string.IsNullOrEmpty(gatewayResult.PaymentIntentId))
                {
                    _logger.LogError("Payment gateway returned invalid result for Invoice {InvoiceId}", invoice.Id);
                    return Result<Guid>.Failure("Failed to generate payment link with the payment provider.");
                }

                // 4. Update Invoice with Gateway Details
                invoice.SetPaymentIntent(gatewayResult.PaymentIntentId);
                
                // If the gateway returns a public URL, we might store that too, but for now we focus on the Intent ID
                // invoice.SetPaymentUrl(gatewayResult.Url); 

                // 5. Persist
                await _repository.AddInvoiceAsync(invoice, cancellationToken);
                await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Invoice {InvoiceId} generated successfully for Project {ProjectId}", invoice.Id, request.ProjectId);

                return Result<Guid>.Success(invoice.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error generating invoice for Project {ProjectId}", request.ProjectId);
                // In a real production scenario, we might want to void the Stripe intent if DB save fails
                return Result<Guid>.Failure("An unexpected error occurred while generating the invoice.");
            }
        }
    }
}