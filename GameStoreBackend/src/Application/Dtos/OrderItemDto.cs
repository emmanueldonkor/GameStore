namespace Application.Dtos;

public record class OrderItemDto
{
    public Guid GameId { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}