using Application.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/{v:apiVersion}/webhook")]
public class WebHookController : ControllerBase
{
    private readonly ISender sender;
    private readonly IConfiguration configuration;
    public WebHookController(ISender sender, IConfiguration configuration)
    {
        this.sender = sender;
        this.configuration = configuration;
    }
    [HttpPost]
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