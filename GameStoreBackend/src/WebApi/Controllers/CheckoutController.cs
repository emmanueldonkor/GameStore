using Application.Checkout;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize(Roles = "User")]
[Route("api/game")]
public class CheckoutController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;

    [HttpPost("checkout")]
    public async Task<IActionResult> CheckOut([FromBody] CreateCheckoutCommand command)
    {
        var result = await sender.Send(command);
        return Ok(new { sessionId = result.SessionId });
    }  
}