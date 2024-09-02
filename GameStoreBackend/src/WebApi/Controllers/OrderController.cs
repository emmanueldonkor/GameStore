using Application.Common.Models;
using Application.Orders.Commands.UpdateOrderStatus;
using Application.Orders.Queries.GetOrdersWithPagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[ApiVersion("1.0")]
[Route("api/{v:apiVersion}/orders")]
public class OrderController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;
    
    [Authorize("read:orders")]
    [HttpGet]
    public async Task<PaginatedList<OrderDto>> GetOrders()
    {
        var query = new GetOrdersWithPaginationQuery();
        return await sender.Send(query);
    }
    [Authorize("read:user-orders")]
    [HttpGet("{userId}")]
    public async Task<ActionResult<PaginatedList<OrderDto>>> GetOrdersById( string userId, [FromQuery] GetOrdersByIdWithPaginationQuery query)
    {
         if(!userId.Equals(query.UserId))
         {
            return BadRequest();
         }
        return await sender.Send(query);
    }
    [Authorize("update:orders")]
    [HttpPost("{orderId:guid}")]
    public async Task<IActionResult> UpdateOrderStatus(Guid orderId, UpdateOrderStatusCommand command)
    {
        if (!orderId.Equals(command.OrderId))
        {
            return BadRequest();
        }
        await sender.Send(command);
        return NoContent();
    }
}