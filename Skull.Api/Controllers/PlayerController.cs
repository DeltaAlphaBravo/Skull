using Microsoft.AspNetCore.Mvc;
using Skull.Api.Models;
using Skull.Skull;

namespace Skull.Api.Controllers
{
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ISkullGame _skullGame;
        private readonly ISkullHub _skullHub;

        public PlayerController(ISkullGame skullGame, SkullHub skullHub)
        {
            _skullGame = skullGame;
            _skullHub = skullHub;
        }

        [HttpPost]
        [Route("api/game/{game}/player")]
        public async Task<ActionResult<IGamePlayerView>> JoinPlayer([FromRoute] string game, [FromBody] IJoinGameModel playerJoin)
        {
            var gameState = await _skullGame.JoinPlayer(game, 
                                                        playerJoin.Name, 
                                                        playerJoin.FirstPlacement == CoasterConstants.SKULL);
            if (gameState == null) return new NotFoundResult();
            var playerId = gameState.Players.Last(p => p.PlayerIdentity?.Name == playerJoin.Name).PlayerId;
            await _skullHub.AddToGroupAsync(game);
            await _skullHub.SendMessageAsync(game, $"{playerJoin.Name} joined \"{game}\" as player {playerId}");
            var view = new OkObjectResult(new GamePlayerView(gameState, playerId));
            return view;
        }

        [HttpGet]
        [Route("api/game/{game}/player/{player}/view")]
        public async Task<ActionResult<IGamePlayerView>> GetGamePlayerViewAsync([FromRoute] string game, [FromRoute] int player)
        {
            var gameState = await _skullGame.GetGameStateAsync(game);
            if (gameState == null) return new NotFoundResult();
            var view = new OkObjectResult(new GamePlayerView(gameState, player));
            return view;
        }

        [HttpPost]
        [Route("api/game/{game}/player/{player}/stack")]
        public async Task<ActionResult<IGamePlayerView>> PlaceCoasterAsync([FromRoute] string game, [FromRoute] int player, [FromBody] bool isSkull)
        {
            try
            {
                var gameState = await _skullGame.PlaceCoasterAsync(game, player, isSkull);
                if (gameState == null) return new NotFoundResult();
                await _skullHub.AddToGroupAsync(game);
                await _skullHub.SendMessageAsync(game, $"player {player} played a coaster");
                return new OkObjectResult(new GamePlayerView(gameState, player));
            }
            catch (InvalidOperationException)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("api/game/{game}/player/{player}/challenge")]
        public async Task<ActionResult<IGamePlayerView>> MakeBidAsync([FromRoute] string game, [FromRoute] int player, [FromBody] int? bid)
        {
            try
            {
                var gameState = await _skullGame.MakeBidAsync(game, player, bid);
                if (gameState == null) return new NotFoundResult();
                await _skullHub.AddToGroupAsync(game);
                await _skullHub.SendMessageAsync(game, $"player {player} challenged with {bid} coasters");
                return new OkObjectResult(new GamePlayerView(gameState, player));
            }
            catch (InvalidOperationException)
            {
                return new BadRequestResult();
            }
        }
    }
}
