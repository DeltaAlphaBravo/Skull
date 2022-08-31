namespace Skull.GamesState;

internal sealed class GameState : IGameState
{
    public int NextPlayer { get; private set; }

    public string Name { get; private init; }

    public IReadOnlyList<IPlayer> Players { get; internal set; }

    public Phase Phase { get; private set; }

    public Stack<IBid> Bids { get; private init; }

    public GameState(string name, IReadOnlyList<IPlayer> players)
    {
        Name = name;
        Players = players;
        Bids = new Stack<IBid>();
        Phase = Phase.Placement;
        NextPlayer = Random.Shared.Next(players.Count - 1);
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
        return Phase;
    }

    public IPlayerState JoinPlayer(string name)
    {
        var newPlayerId = Players.Count;
        Players[newPlayerId].AttachIdentity(name);
        return Players[newPlayerId].PlayerState;
    }
}
