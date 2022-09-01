namespace Skull.Api.Models
{
    public interface IOpponentState
    {
        int PlayerId { get; }
        string Name { get; }
        IOpponentHand Hand { get; }
        int StackCount { get; }
    }
}
