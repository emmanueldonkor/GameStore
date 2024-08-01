
using System.ComponentModel;

namespace Domain.Entities;

public class Game
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public decimal Price { get; set; }
    public required string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}