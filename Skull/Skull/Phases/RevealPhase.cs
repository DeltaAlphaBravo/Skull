using Skull.GamesState;

namespace Skull.Phases;

internal sealed class RevealPhase
{
    public IGameState GameState { get; }
    public RevealPhase(IGameState gameState)
    {
        GameState = gameState;
    }
}
