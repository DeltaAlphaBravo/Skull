namespace Skull.GamesState
{
    public interface IPlayerState
    {
        int PlayerId { get; }
        IHand Hand { get; }
        Stack<Coaster> PlayedCoasters { get; }
    }
}
