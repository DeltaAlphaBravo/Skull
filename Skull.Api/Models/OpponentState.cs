namespace Skull.Api.Models
{
    public sealed class OpponentState : IOpponentState
    {
        public int PlayerId { get; init; }

        public IOpponentHand Hand { get; init; }

        public OpponentState(int playerId, IOpponentHand hand)
        {
            PlayerId = playerId;
            Hand = hand;
        }

        public int StackCount { get; init; }
    }
}
