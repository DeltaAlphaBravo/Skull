using SkullApi.Models.Internal.GamesState;

namespace SkullApi.Models.Internal.Phases
{
    public sealed class RevealPhase
    {
        public IGameState GameState { get; }
        public RevealPhase(IGameState gameState)
        {
            GameState = gameState;
        }
    }
}
