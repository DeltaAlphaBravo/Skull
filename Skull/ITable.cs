using Skull.GamesState;
using Skull.GamesState.Player;

namespace Skull;

public interface ITable
{
    string Name { get; }
    IGameState? Game { get; }
    IReadOnlyList<IPlayerIdentity> Players { get; }
    int JoinPlayer(string name);
}
