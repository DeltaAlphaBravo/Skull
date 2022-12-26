namespace Skull.Api.Models
{
    public interface IOpponentState
    {
        int PlayerId { get; }
        IOpponentHand Hand { get; }
        int StackCount { get; }
        IEnumerable<bool> Reveals { get; }
    }
}
