using Application.Common.Models;
using Application.Games.Commands.CreateGame;
using Application.Games.Commands.DeleteGame;
using Application.Games.Commands.UpdateGame;
using Application.Games.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/game")]
public class GameController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;

    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<PaginatedList<GameDto>> GetGamesWithPagination([FromQuery] GetGamesWithPaginationQuery query)
    {
        return await sender.Send(query);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateGame(CreateGameCommand command)
    {
        var newGame = await sender.Send(command);
        return CreatedAtAction(nameof(GetGame), new { id = newGame.Id }, newGame);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGame(Guid id)
    {
        var gameQuery = new GetGameByIdQuery(id);
        var result = await sender.Send(gameQuery);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGame(Guid id, UpdateGameCommand command)
    {
        if (!id.Equals(command.GameId))
        {
            return BadRequest();
        }
        await sender.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        var deleteGameCommand = new DeleteGameCommand(id);
        await sender.Send(deleteGameCommand);
        return NoContent();
    }

}