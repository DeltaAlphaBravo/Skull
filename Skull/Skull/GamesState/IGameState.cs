namespace Skull.GamesState
{
    public interface IGameState
    {
        IReadOnlyList<IPlayer> Players { get; }
        int NextPlayer { get; }
        string Name { get; }
        Phase Phase { get; }
        Stack<IBid> Bids { get; }
        int GoToNextPlayer();
        Phase GoToNextPhase();
        IPlayerState JoinPlayer(string name);
    }
}