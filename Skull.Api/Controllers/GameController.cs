using Microsoft.AspNetCore.Mvc;
using Skull.GamesState;

namespace Skull.Api.Controllers;

[ApiController]
public class GameController : ControllerBase
{
    private readonly ISkullGame _skullGame;
    private readonly ISkullHub _skullHub;
    private readonly ITableRepository _tableRepository;

    public GameController(ISkullGame skullGame, 
                          ISkullHub skullHub, 
                          ITableRepository tableRepository)
    {
        _skullGame = skullGame;
        _skullHub = skullHub;
        _tableRepository = tableRepository;
    }

    [HttpPost]
    [Route("api/table/{tableName}/game")]
    public async Task<IGameState> CreateNewGameAsync([FromRoute] string tableName)
    {
        var table = await _tableRepository.GetTableAsync(tableName);
        var gameState = await _skullGame.StartGameAsync(table);
        await _skullHub.SendMessageAsync(tableName, $"{tableName} started a new game");
        return gameState;
    }
}
