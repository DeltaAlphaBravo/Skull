using Skull.GamesState.GamePlay;
using Skull.GamesState.Player;

namespace Skull.GamesState
{
    public interface IGameState
    {
        IReadOnlyList<IPlayerState> PlayerStates { get; }
        int NextPlayer { get; }
        Phase Phase { get; }
        Stack<IBid> Bids { get; }
        Stack<IRevealedCoaster> Reveals { get; }
        int GoToNextPlayer();
        Phase GoToNextPhase();
        void HandleVictory();
        void HandleLoss();
    }
}