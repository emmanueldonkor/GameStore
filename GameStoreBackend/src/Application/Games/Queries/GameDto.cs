using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Games.Queries;

public record class GameDto : IMapFrom<Game>
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Genre { get; init; }
    public decimal Price { get; init; }
    public required string ImageUrl { get; init; }
    public DateTime? CreatedAt { get; init; }
}