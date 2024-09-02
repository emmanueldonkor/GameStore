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
[ApiVersion("1.0")]
[Route("api/{v:apiversion}/games")]
public class GameController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;

    [HttpGet]
    public async Task<PaginatedList<GameDto>> GetGamesWithPagination([FromQuery] GetGamesWithPaginationQuery query)
    {
        return await sender.Send(query);
    }
    [Authorize("create:games")]
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

    [Authorize("update:games")]
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

    [Authorize("delete:games")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        var deleteGameCommand = new DeleteGameCommand(id);
        await sender.Send(deleteGameCommand);
        return NoContent();
    }

}