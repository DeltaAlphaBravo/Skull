namespace SkullApi.Models.Internal.GamesState
{
    public interface IGameState
    {
        IList<IPlayer> Players { get; }
        int PlayerWithOnus { get; }
        string Name { get; }
        Phase Phase { get; }
        Stack<IBid> Bids { get; }
        int NextPlayer();
        Phase NextPhase();
    }
}