using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ISender sender;

    public OrderController(ISender sender)
    {
        this.sender = sender;
    }
    
}