using Domain.Entities;

namespace Application.Dtos;

public record class OrderDto
{
    public Guid UserId { get; init; }
    public DateTime OrderDate { get; init; }
    public decimal TotalAmount { get; init; }
    public OrderStatus Status { get; init; }
}