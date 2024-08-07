using Application.Checkout;
using Application.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace WebApi.Controllers;

[ApiController]
[Route("api/game")]
public class CheckoutController : ControllerBase
{
    private readonly ISender sender;
    private readonly IConfiguration configuration;

    public CheckoutController(ISender sender, IConfiguration configuration)
    {
        this.sender = sender;
        this.configuration = configuration;
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> CheckOut([FromBody] CreateCheckoutCommand command)
    {
        var checkout = new CreateCheckoutCommand
        {
            UserId = command.UserId,
            TotalAmount = command.TotalAmount,
            OrderItems = command.OrderItems,
            ShippingAddress = command.ShippingAddress,
            SuccessUrl = command.SuccessUrl,
            CancelUrl = command.CancelUrl
        };
        var result = await sender.Send(command);
        return Ok(new { sessionId = result.SessionId });
    }
    [HttpPost("webhook")]
    public async Task<IActionResult> HandleStripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                configuration["Stripe:WebhookSecret"]
            );

            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                var command = new HandlePaymentSuccessCommand { Session = session ?? throw new ArgumentNullException(nameof(session)) };
                await sender.Send(command);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest(e.Message);
        }
    }

}