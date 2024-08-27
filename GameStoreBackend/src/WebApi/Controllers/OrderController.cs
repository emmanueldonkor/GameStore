using Application.Common.Models;
using Application.Orders.Commands.UpdateOrderStatus;
using Application.Orders.Queries.GetOrdersWithPagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

 [Authorize(Roles = "Admin")]
[ApiController]
[Route("api/order")]
public class OrderController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;
    
    [HttpGet]
    public async Task<PaginatedList<OrderDto>> GetOrders()
    {
        var query = new GetOrdersWithPaginationQuery();
        return await sender.Send(query);
    }

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