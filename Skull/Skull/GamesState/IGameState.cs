using Skull.Skull.GamesState.GamePlay;
using Skull.Skull.GamesState.Player;

namespace Skull.Skull.GamesState
{
    public interface IGameState
    {
        IReadOnlyList<IPlayer> Players { get; }
        int NextPlayer { get; }
        string Name { get; }
        Phase Phase { get; }
        Stack<IBid> Bids { get; }
        int RandomNextPlayer();
        int GoToNextPlayer();
        Phase GoToNextPhase();
        int JoinPlayer(string name);
    }
}