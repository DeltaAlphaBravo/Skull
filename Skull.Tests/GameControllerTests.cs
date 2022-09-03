using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skull.GamesState;
using Skull.Tests;
using System.Linq;
using System.Threading.Tasks;

namespace Skull.Api.Tests
{
    [TestClass]
    public class SkullGameTests
    {
        [TestMethod]
        public async Task GivenNewGameRequestReceived_WhenRequestForThreePlayers_ThenNewGameReturned()
        {
            //Arrange
            var target = new SkullGame(new SingleGameInMemoryGameStateRepository());

            //Act
            var actual = await target.CreateGameAsync(3);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Players.Count);
        }

        [TestMethod]
        public async Task GivenPlacementPhaseAndPlayerHasSkull_WhenPlayerPlaysSkull_ThenSkullMovedToStack()
        {
            //Arrange
            var (target, gameState) = await CreateNPlayerGame(3);
            var actingPlayer = gameState.NextPlayer;

            //Act
            await target.PlaceCoasterAsync(gameState.Name, actingPlayer, true);

            //Assert
            gameState = await target.GetGameStateAsync(gameState.Name);
            Assert.IsNotNull(gameState);
            Assert.IsFalse(gameState.Players[actingPlayer].PlayerState.Hand.HasSkull);
            Assert.AreEqual(1, gameState.Players[actingPlayer].PlayerState.PlayedCoasters.Count(s => s == Coaster.Skull));
            Assert.AreEqual(2, gameState.Players[actingPlayer].PlayerState.Hand.CardCount);
        }

        [TestMethod]
        public async Task GivenChallengePhase_WhenPlayersPass_ThenNextPlayerChosen()
        {
            //Arrange
            var repository = new SingleGameInMemoryGameStateRepository();
            var (target, gameState) = await CreateNPlayerGame(3, repository);

            var firstPlayer = gameState.NextPlayer;
            for (int i = firstPlayer; i < firstPlayer + 3; i++) await target.PlaceCoasterAsync(gameState.Name, i % 3, false);
            var challengingPlayer = gameState.NextPlayer;

            //Act
            var currentPlayer = (await target.MakeBidAsync(gameState.Name, challengingPlayer, 1))!.NextPlayer;
            var challengeHighBidPlayer = (await target.MakeBidAsync(gameState.Name, currentPlayer, null))!.NextPlayer;
            currentPlayer = (await target.MakeBidAsync(gameState.Name, challengeHighBidPlayer, 2))!.NextPlayer;
            currentPlayer = (await target.MakeBidAsync(gameState.Name, currentPlayer, null))!.NextPlayer;

            //Assert
            Assert.AreEqual(challengeHighBidPlayer, currentPlayer);
            Assert.AreEqual(Phase.Reveal, (await repository.GetGameStateAsync(gameState.Name))!.Phase);
        }

        [TestMethod]
        public async Task GivenChallengePhase_WhenAPlayerDoesAMaxBid_ThenRevealPhaseBegins()
        {
            //Arrange
            var repository = new SingleGameInMemoryGameStateRepository();
            var (target, gameState) = await CreateNPlayerGame(3, repository);

            var firstPlayer = gameState.NextPlayer;
            for (int i = firstPlayer; i < firstPlayer + 3; i++) await target.PlaceCoasterAsync(gameState.Name, i % 3, false);
            for (int i = firstPlayer; i < firstPlayer + 3; i++) await target.PlaceCoasterAsync(gameState.Name, i % 3, true);

            //Act
            var currentPlayer = (await target.MakeBidAsync(gameState.Name, firstPlayer, 9))!.NextPlayer;

            //Assert
            Assert.AreEqual(Phase.Reveal, (await repository.GetGameStateAsync(gameState.Name))!.Phase);
        }

        private static async Task<(SkullGame, IGameState)> CreateNPlayerGame(int n, IGameStateRepository gameStateRepository)
        {
            var target = new SkullGame(gameStateRepository);
            var gameState = await target.CreateGameAsync(n);
            for (int i = 0; i < n; i++) await target.JoinPlayer(gameState.Name, i.ToString(), false);
            return (target, gameState);
        }

        private static async Task<(SkullGame, IGameState)> CreateNPlayerGame(int n)
        {
            return await CreateNPlayerGame(n, new SingleGameInMemoryGameStateRepository());
        }
    }
}