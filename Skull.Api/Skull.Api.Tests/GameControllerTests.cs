using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkullApi.Controllers;
using SkullApi.Models.External;
using SkullApi.Models.Internal;
using SkullApi.Models.Internal.GamesState;
using System.Linq;
using System.Threading.Tasks;

namespace SkullApi.Tests
{
    [TestClass]
    public class GameControllerTests
    {
        [TestMethod]
        public async Task GivenNewGameRequestReceived_WhenRequestForThreePlayers_ThenNewGameReturned()
        {
            //Arrange
            var skullGame = new SkullGame(new SingleGameInMemoryGameStateRepository());
            var target = new GameController(skullGame);

            //Act
            var actual = await target.CreateNewGameAsync(3);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Players.Count);
        }

        [TestMethod]
        public async Task GivenPlacementPhaseAndPlayerHasSkull_WhenPlayerPlaysSkull_ThenSkullMovedToStack()
        {
            //Arrange
            var skullGame = new SkullGame(new SingleGameInMemoryGameStateRepository());

            var gameState = await new GameController(skullGame).CreateNewGameAsync(3);
            var target = new PlayerController(skullGame);
            var actingPlayer = gameState.PlayerWithOnus;

            //Act
            var playerView = GetGamePlayerView(await target.PlaceCoasterAsync(gameState.Name, actingPlayer, true));

            //Assert
            playerView = GetGamePlayerView(await target.GetGamePlayerViewAsync(gameState.Name, actingPlayer));
            Assert.IsNotNull(playerView);
            Assert.IsFalse(playerView.Hand.HasSkull);
            Assert.AreEqual(1, playerView.PlayedCoasters.Count(s => s == true));
        }

        [TestMethod]
        public async Task GivenChallengePhase_WhenPlayersPass_ThenNextPlayerChosen()
        {
            //Arrange
            var repository = new SingleGameInMemoryGameStateRepository();
            var skullGame = new SkullGame(repository);
            var gameState = await new GameController(skullGame).CreateNewGameAsync(3);
            var target = new PlayerController(skullGame);

            for (int i = 0; i < 3; i++) await target.PlaceCoasterAsync(gameState.Name, i, false);
            var challengingPlayer = gameState.PlayerWithOnus;

            //Act
            var currentPlayer = GetGamePlayerView(await target.MakeBidAsync(gameState.Name, challengingPlayer, 1))!.PlayerWithOnus;
            var challengeHighBidPlayer = GetGamePlayerView(await target.MakeBidAsync(gameState.Name, currentPlayer, null))!.PlayerWithOnus;
            currentPlayer = GetGamePlayerView(await target.MakeBidAsync(gameState.Name, challengeHighBidPlayer, 2))!.PlayerWithOnus;
            currentPlayer = GetGamePlayerView(await target.MakeBidAsync(gameState.Name, currentPlayer, null))!.PlayerWithOnus;

            //Assert
            Assert.AreEqual(challengeHighBidPlayer, currentPlayer);
            Assert.AreEqual(Phase.Reveal, (await repository.GetGameStateAsync(gameState.Name))!.Phase);
        }

        [TestMethod]
        public async Task GivenChallengePhase_WhenAPlayerDoesAMaxBid_ThenRevealPhaseBegins()
        {
            //Arrange
            var repository = new SingleGameInMemoryGameStateRepository();
            var skullGame = new SkullGame(repository);
            var gameState = await new GameController(skullGame).CreateNewGameAsync(3);
            var target = new PlayerController(skullGame);

            var firstPlayer = gameState.PlayerWithOnus;
            for (int i = firstPlayer; i < firstPlayer + 3; i++) await target.PlaceCoasterAsync(gameState.Name, i % 3, false);
            for (int i = firstPlayer; i < firstPlayer + 3; i++) await target.PlaceCoasterAsync(gameState.Name, i % 3, true);

            //Act
            var currentPlayer = GetGamePlayerView(await target.MakeBidAsync(gameState.Name, firstPlayer, 6))!.PlayerWithOnus;

            //Assert
            Assert.AreEqual(Phase.Reveal, (await repository.GetGameStateAsync(gameState.Name))!.Phase);
        }

        private static IGamePlayerView? GetGamePlayerView(ActionResult<IGamePlayerView> actionResult) 
            => (actionResult.Result as OkObjectResult)?.Value as IGamePlayerView;
    }
}