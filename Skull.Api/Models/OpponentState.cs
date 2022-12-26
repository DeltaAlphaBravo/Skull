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
            Reveals = new List<bool>();
        }

        public int StackCount { get; init; }

        public IEnumerable<bool> Reveals { get; init; }
    }
}
