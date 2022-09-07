using Microsoft.AspNetCore.Mvc;
using Skull.Api.Models;

namespace Skull.Api.Controllers
{
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ISkullGame _skullGame;
        private readonly ISkullHub _skullHub;

        public PlayerController(ISkullGame skullGame, ISkullHub skullHub)
        {
            _skullGame = skullGame;
            _skullHub = skullHub;
        }

        [HttpGet]
        [Route("api/table/{tableName}/player/{player}/view")]
        public async Task<ActionResult<IGamePlayerView>> GetGamePlayerViewAsync([FromRoute] string tableName, [FromRoute] int player)
        {
            var gameState = await _skullGame.GetGameStateAsync(tableName);
            if (gameState == null) return new NotFoundResult();
            var view = new OkObjectResult(new GamePlayerView(gameState, player));
            return view;
        }

        [HttpPost]
        [Route("api/table/{tableName}/player/{player}/stack")]
        public async Task<ActionResult<IGamePlayerView>> PlaceCoasterAsync([FromRoute] string tableName, [FromRoute] int player, [FromBody] bool isSkull)
        {
            try
            {
                var gameState = await _skullGame.PlaceCoasterAsync(tableName, player, isSkull);
                if (gameState == null) return new NotFoundResult();
                await _skullHub.SendMessageAsync(tableName, $"player {player} played a coaster");
                return new OkObjectResult(new GamePlayerView(gameState, player));
            }
            catch (InvalidOperationException)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("api/table/{tableName}/player/{player}/challenge")]
        public async Task<ActionResult<IGamePlayerView>> MakeBidAsync([FromRoute] string tableName, [FromRoute] int player, [FromBody] int? bid)
        {
            try
            {
                var gameState = await _skullGame.MakeBidAsync(tableName, player, bid);
                if (gameState == null) return new NotFoundResult();
                await _skullHub.SendMessageAsync(tableName, $"player {player} challenged with {bid} coasters");
                return new OkObjectResult(new GamePlayerView(gameState, player));
            }
            catch (InvalidOperationException)
            {
                return new BadRequestResult();
            }
        }
    }
}
