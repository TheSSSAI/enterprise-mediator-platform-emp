using EnterpriseMediator.Financial.Application.Features.Payments.EventHandlers;
using EnterpriseMediator.Financial.Infrastructure.Persistence.Configurations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace EnterpriseMediator.Financial.Web.API.Controllers.Webhooks
{
    [ApiController]
    [Route("api/webhooks/stripe")]
    [AllowAnonymous] // Stripe calls this endpoint, so no JWT auth
    public class StripeWebhookController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly StripeSettings _stripeSettings;
        private readonly ILogger<StripeWebhookController> _logger;

        public StripeWebhookController(
            ISender sender,
            IOptions<StripeSettings> stripeSettings,
            ILogger<StripeWebhookController> logger)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _stripeSettings = stripeSettings?.Value ?? throw new ArgumentNullException(nameof(stripeSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles incoming webhooks from Stripe.
        /// </summary>
        /// <remarks>
        /// Validates the Stripe signature and dispatches relevant internal commands 
        /// based on the event type (e.g., payment_intent.succeeded).
        /// </remarks>
        /// <returns>200 OK if processed or ignored, 400 if invalid signature.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> HandleWebhook()
        {
            string json;
            try
            {
                using var reader = new StreamReader(HttpContext.Request.Body);
                json = await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to read Stripe webhook body.");
                return BadRequest("Could not read request body.");
            }

            // Verify Stripe Signature
            if (!Request.Headers.TryGetValue("Stripe-Signature", out var signatureHeader))
            {
                _logger.LogWarning("Stripe webhook missing signature header.");
                return BadRequest("Missing Stripe-Signature header.");
            }

            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    signatureHeader,
                    _stripeSettings.WebhookSecret
                );
            }
            catch (StripeException ex)
            {
                _logger.LogWarning(ex, "Stripe signature verification failed.");
                return BadRequest("Invalid Stripe signature.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error constructing Stripe event.");
                return StatusCode(500, "Internal server error processing webhook.");
            }

            _logger.LogInformation("Received Stripe Webhook: {EventType} | ID: {EventId}", stripeEvent.Type, stripeEvent.Id);

            try
            {
                switch (stripeEvent.Type)
                {
                    case Events.PaymentIntentSucceeded:
                        if (stripeEvent.Data.Object is PaymentIntent paymentIntent)
                        {
                            _logger.LogInformation("Processing PaymentIntent Succeeded: {PaymentIntentId}", paymentIntent.Id);
                            
                            // Map the Stripe event to an internal application command
                            // Assuming PaymentSucceededCommand exists in the Application layer as per SDS requirements US-058
                            // Since we cannot verify the exact namespace from the prompt's file list, we define the contract locally if needed
                            // or instantiate the command we expect the handler to use.
                            
                            // Note: StripeWebhookHandler (Level 4) is expected to handle this.
                            // We dispatch a generic notification or specific command. 
                            // Based on Clean Architecture, we map to an internal Command.
                            
                            var command = new ProcessStripeEventCommand(
                                stripeEvent.Id,
                                stripeEvent.Type,
                                paymentIntent.Id,
                                paymentIntent.Amount,
                                paymentIntent.Currency,
                                paymentIntent.Metadata
                            );

                            await _sender.Send(command);
                        }
                        break;

                    case Events.PaymentIntentPaymentFailed:
                        if (stripeEvent.Data.Object is PaymentIntent failedIntent)
                        {
                            _logger.LogWarning("PaymentIntent Failed: {PaymentIntentId} | Reason: {FailureReason}", 
                                failedIntent.Id, failedIntent.LastPaymentError?.Message);
                            
                            // Logic for failure handling could be dispatched here if requirements dictate
                        }
                        break;

                    default:
                        // Log unhandled event types as information/debug
                        _logger.LogDebug("Unhandled Stripe event type: {EventType}", stripeEvent.Type);
                        break;
                }

                // Return 200 OK to Stripe to acknowledge receipt
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Stripe webhook event {EventId}", stripeEvent.Id);
                // Return 500 to signal Stripe to retry the webhook later
                return StatusCode(500, "Error processing event.");
            }
        }
    }

    // Command definition expected by the Application layer logic.
    // Placed here for context if not explicitly defined in Level 3 files, 
    // but typically this would be in Application/Features/Payments/Commands.
    public record ProcessStripeEventCommand(
        string EventId,
        string EventType,
        string PaymentIntentId,
        long Amount,
        string Currency,
        Dictionary<string, string> Metadata
    ) : IRequest;
}