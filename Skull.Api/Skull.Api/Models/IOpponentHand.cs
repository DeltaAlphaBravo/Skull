namespace Skull.Api.Models
{
    public interface IOpponentHand
    {
        int CardCount { get; }
        int PlayerId { get; }
    }
}