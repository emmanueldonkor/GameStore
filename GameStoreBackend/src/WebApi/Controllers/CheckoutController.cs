using Application.Checkout;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Authorize(Roles = "User")]
[Route("api/v{version:apiVersion}/checkout")]
public class CheckoutController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;

    [HttpPost]
    public async Task<IActionResult> CheckOut([FromBody] CreateCheckoutCommand command)
    {
        var result = await sender.Send(command);
        return Ok(new { sessionId = result.SessionId });
    }  
}