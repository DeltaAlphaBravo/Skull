using Skull.Skull.GamesState;

namespace Skull.Skull.Phases;

internal sealed class RevealPhase
{
    public IGameState GameState { get; }
    public RevealPhase(IGameState gameState)
    {
        GameState = gameState;
    }
}
