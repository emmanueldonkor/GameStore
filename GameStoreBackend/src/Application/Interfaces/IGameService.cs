using Domain.Entities;

namespace Application.Interfaces;

public interface IGameService
{
    Task<IEnumerable<Game>> GetGameListAsync();
    Task<Game?> GetGameByIdAsync(Guid id);
    Task CreateGameAsync(Game game);
     Task UpdateGameAsync(Game updatedGame);
     Task DeleteGameAsync(Guid id);
}