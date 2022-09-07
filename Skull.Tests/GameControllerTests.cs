using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skull.GamesState;
using System.Linq;
using System.Threading.Tasks;

namespace Skull.Tests
{
    [TestClass]
    public class SkullGameTests
    {
        [TestMethod]
        public async Task GivenThreePlayerTable_WhenGameCreated_ThenNewGameReturned()
        {
            //Arrange
            //Act
            var actual = await CreateNPlayerGameAsync("Test", 3);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Item2.PlayerStates.Count);
        }

        [TestMethod]
        public async Task GivenPlacementPhaseAndPlayerHasSkull_WhenPlayerPlaysSkull_ThenSkullMovedToStack()
        {
            //Arrange
            const string TableName = "Test";
            var (target, gameState) = await CreateNPlayerGameAsync(TableName, 3);
            var actingPlayer = gameState.NextPlayer;

            //Act
            await target.PlaceCoasterAsync(TableName, actingPlayer, true);

            //Assert
            gameState = await target.GetGameStateAsync(TableName);
            Assert.IsNotNull(gameState);
            Assert.IsFalse(gameState.PlayerStates[actingPlayer].Hand.HasSkull);
            Assert.AreEqual(1, gameState.PlayerStates[actingPlayer].PlayedCoasters.Count(s => s == Coaster.Skull));
            Assert.AreEqual(3, gameState.PlayerStates[actingPlayer].Hand.CardCount);
        }

        [TestMethod]
        public async Task GivenChallengePhase_WhenPlayersPass_ThenNextPlayerChosen()
        {
            //Arrange
            const string TableName = "Test";
            var repository = new SingleGameInMemoryGameStateRepository();
            var (target, gameState) = await CreateNPlayerGameAsync(TableName, 3, repository);

            var firstPlayer = gameState.NextPlayer;
            for (int i = firstPlayer; i < firstPlayer + 3; i++) await target.PlaceCoasterAsync(TableName, i % 3, false);
            var challengingPlayer = gameState.NextPlayer;

            //Act
            var currentPlayer = (await target.MakeBidAsync(TableName, challengingPlayer, 1))!.NextPlayer;
            var challengeHighBidPlayer = (await target.MakeBidAsync(TableName, currentPlayer, null))!.NextPlayer;
            currentPlayer = (await target.MakeBidAsync(TableName, challengeHighBidPlayer, 2))!.NextPlayer;
            currentPlayer = (await target.MakeBidAsync(TableName, currentPlayer, null))!.NextPlayer;

            //Assert
            Assert.AreEqual(challengeHighBidPlayer, currentPlayer);
            Assert.AreEqual(Phase.Reveal, (await repository.GetGameStateAsync(TableName))!.Phase);
        }

        [TestMethod]
        public async Task GivenChallengePhase_WhenAPlayerDoesAMaxBid_ThenRevealPhaseBegins()
        {
            //Arrange
            const string TableName = "Test";
            var repository = new SingleGameInMemoryGameStateRepository();
            var (target, gameState) = await CreateNPlayerGameAsync(TableName, 3, repository);

            var firstPlayer = gameState.NextPlayer;
            for (int i = firstPlayer; i < firstPlayer + 3; i++) await target.PlaceCoasterAsync(TableName, i % 3, false);
            for (int i = firstPlayer; i < firstPlayer + 3; i++) await target.PlaceCoasterAsync(TableName, i % 3, true);

            //Act
            var currentPlayer = (await target.MakeBidAsync(TableName, firstPlayer, 6))!.NextPlayer;

            //Assert
            Assert.AreEqual(Phase.Reveal, (await repository.GetGameStateAsync(TableName))!.Phase);
        }

        private static async Task<(SkullGame, IGameState)> CreateNPlayerGameAsync(string tableName, int n, IGameStateRepository gameStateRepository)
        {
            var table = new Table(tableName);
            for (int i = 0; i < n; i++) table.JoinPlayer(i.ToString());
            var target = new SkullGame(gameStateRepository);
            var gameState = await target.StartGameAsync(table);
            return (target, gameState);
        }

        private static async Task<(SkullGame, IGameState)> CreateNPlayerGameAsync(string tableName, int n)
        {
            return await CreateNPlayerGameAsync(tableName, n, new SingleGameInMemoryGameStateRepository());
        }
    }
}