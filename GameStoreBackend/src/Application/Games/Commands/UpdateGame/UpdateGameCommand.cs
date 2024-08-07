using Application.Interfaces;
using MediatR;

namespace Application.Games.Commands.Update;

public record class UpdateGameCommand : IRequest
{
    public Guid GameId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? Category { get; init; }
    public decimal? Price { get; init; }
    public string? ImageUrl { get; init; }
}

public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand>
{
    private readonly IApplicationDbContext context;

    public UpdateGameCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(UpdateGameCommand request, CancellationToken ct = default)
    {
        var existingGame = await context.Games.FindAsync([request.GameId], cancellationToken: ct);
        if (existingGame is null)
        {
            throw new NullReferenceException("Game not found");
        }
        existingGame.Name = request.Name ?? existingGame.Name;
        existingGame.Description = request.Description ?? existingGame.Description;
        existingGame.Genre = request.Category ?? existingGame.Genre;
        existingGame.Price = request.Price ?? existingGame.Price;
        existingGame.ImageUrl = request.ImageUrl ?? existingGame.ImageUrl;
        await context.SaveChangesAsync(ct);
    }
}
