using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Games.Commands.CreateGame;

public record class CreateGameCommand : IRequest<Game>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Category { get; init; }
    public decimal Price { get; init; }
    public required string ImageUrl { get; init; }
}

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Game>
{
    private readonly IApplicationDbContext dbContext;

    public CreateGameCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Game> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        Game? game = new()
        {
            Name = request.Name,
            Description = request.Description,
            Genre = request.Category,
            Price = request.Price,
            ImageUrl = request.ImageUrl,
        };
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync(cancellationToken);
        return game;
    }
}