using Skull.Skull.GamesState.GamePlay;
using Skull.Skull.GamesState.Player;

namespace Skull.Skull.GamesState;

internal sealed class GameState : IGameState
{
    private readonly IList<IPlayer> _players;
    public int NextPlayer { get; private set; }

    public string Name { get; private init; }

    public IReadOnlyList<IPlayer> Players { get => (IReadOnlyList<IPlayer>)_players; }

    public Phase Phase { get; private set; }

    public Stack<IBid> Bids { get; private init; }

    public GameState(string name)
    {
        Name = name;
        _players = new List<IPlayer>();
        Bids = new Stack<IBid>();
        Phase = Phase.Creation;
        NextPlayer = Random.Shared.Next(_players.Count - 1);
    }

    public int GoToNextPlayer()
    {
        NextPlayer++;
        if (NextPlayer == Players.Count) NextPlayer = 0;
        return NextPlayer;
    }

    public Phase GoToNextPhase()
    {
        if (Phase == Phase.Reveal) Phase = Phase.Complete;
        if (Phase == Phase.Challenge) Phase = Phase.Reveal;
        if (Phase == Phase.Placement) Phase = Phase.Challenge;
        if (Phase == Phase.Creation) Phase = Phase.Placement;
        return Phase;
    }

    public int JoinPlayer(string name)
    {
        var playerId = _players.Count;
        var player = new Player.Player(playerId);
        player.AttachIdentity(name);
        _players.Add(player);
        return playerId;
    }
}
