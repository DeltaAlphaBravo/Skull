namespace Skull.GamesState
{
    public interface IPlayerState
    {
        IHand Hand { get; }
        Stack<Coaster> PlayedCoasters { get; }
    }
}
