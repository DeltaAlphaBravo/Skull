using Skull.GamesState.GamePlay;
using Skull.GamesState.Player;

namespace Skull.GamesState;

internal sealed class GameState : IGameState
{
    private readonly IList<IPlayerState> _players;
    public int NextPlayer { get; private set; }

    public IReadOnlyList<IPlayerState> PlayerStates { get => (IReadOnlyList<IPlayerState>)_players; }

    public Phase Phase { get; private set; }

    public Stack<IBid> Bids { get; private init; }

    public Stack<int> Reveals { get; private init; }

    public GameState(int playerCount)
    {
        _players = Enumerable.Range(0, playerCount)
                             .Select(y => new PlayerState(y))
                             .ToList<IPlayerState>();
        Bids = new Stack<IBid>();
        Reveals = new Stack<int>();
        Phase = Phase.Placement;
        RandomNextPlayer();
    }

    private int RandomNextPlayer()
    {
        NextPlayer = Random.Shared.Next(_players.Count - 1);
        return NextPlayer;
    }

    public int GoToNextPlayer()
    {
        NextPlayer++;
        if (NextPlayer == PlayerStates.Count) NextPlayer = 0;
        return NextPlayer;
    }

    public Phase GoToNextPhase()
    {
        if (Phase == Phase.Reveal) Phase = Phase.Complete;
        if (Phase == Phase.Challenge) Phase = Phase.Reveal;
        if (Phase == Phase.Placement) Phase = Phase.Challenge;
        return Phase;
    }

    public void HandleVictory()
    {
        PlayerStates[NextPlayer].RecordVictory();
    }

    public void HandleLoss()
    {
        throw new NotImplementedException();
    }
}
