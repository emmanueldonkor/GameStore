using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Game
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Genre { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public required string ImageUrl { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [JsonIgnore]
    public IReadOnlyList<OrderItem>? OrderItems { get; set; }
}