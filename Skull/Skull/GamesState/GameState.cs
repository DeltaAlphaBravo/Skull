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
        Phase = Phase.Creation;
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
        if (Phase == Phase.Creation) Phase = Phase.Placement;
        return Phase;
    }

    public int JoinPlayer(string name)
    {
        var newPlayerId = Players.Where(p => p.PlayerIdentity == null)
                                 .Min(p => p.PlayerId);
        Players[newPlayerId].AttachIdentity(name);
        return newPlayerId;
    }
}
