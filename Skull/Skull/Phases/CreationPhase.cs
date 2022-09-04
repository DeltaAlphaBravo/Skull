using Skull.Skull.GamesState;
using Skull.Skull.GamesState.Player;

namespace Skull.Skull.Phases
{
    internal sealed class CreationPhase : ICreationPhase
    {
        public IGameState GameState { get; private set; }
        private CreationPhase(IGameState gameState)
        {
            GameState = gameState;
        }

        public static IGameState CreateGame(string name)
        {
            var gameState = new GameState(name);
            return gameState;
        }

        public int JoinPlayer(string name)
        {
            return GameState.JoinPlayer(name);
        }

        public IGameState PlaceFirstCoaster(int player, bool isSkull)
        {
            var playerState = isSkull switch
            {
                true => GameState.Players.Single(p => p.PlayerId == player).PlaySkull(),
                false => GameState.Players.Single(p => p.PlayerId == player).PlayFlower()
            };
            return GameState;
        }

        public static ICreationPhase CreateFromState(IGameState gameStatus)
        {
            if (gameStatus.Phase != Phase.Creation) throw new InvalidOperationException();
            return new CreationPhase(gameStatus);
        }
    }
}
