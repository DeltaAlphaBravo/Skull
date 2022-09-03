namespace Skull.Api.Models
{
    public interface IGamePlayerView
    {
        int PlayerId { get; }
        int NextPlayer { get; }
        IEnumerable<IOpponentState> OpponentStates { get; }
        IReadOnlyHand Hand { get; }
        Stack<bool> PlayedCoasters { get; }
    }
}
