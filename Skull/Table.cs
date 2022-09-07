using Skull.GamesState;
using Skull.GamesState.Player;

namespace Skull;

public class Table : ITable
{
    private readonly List<IPlayerIdentity> _players;
    public string Name { get; private init; }

    public IGameState? Game { get; private set; }

    public IReadOnlyList<IPlayerIdentity> Players => _players;

    public Table(string name)
    {
        Name = name;
        _players = new List<IPlayerIdentity>();
    }

    public int JoinPlayer(string name)
    {
        var playerId = _players.Count;
        var player = new PlayerIdentity(name, playerId);
        _players.Add(player);
        return playerId;
    }
}