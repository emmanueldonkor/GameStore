using Domain.Entities;

namespace Application.Dtos;

public record class CheckoutDto
{
    public required Guid UserId { get; init; }
    public required List<OrderItemDto> OrderItems { get; init; }
    public required ShippingAddressDto ShippingAddress { get; init; }
    public required decimal TotalAmount { get; init; } 
    public required string SuccessUrl { get; init; }
    public required string CancelUrl { get; init; }
}
