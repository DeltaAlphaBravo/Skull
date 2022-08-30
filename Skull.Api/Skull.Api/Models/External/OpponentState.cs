namespace SkullApi.Models.External
{
    public sealed class OpponentState : IOpponentState
    {
        public int PlayerId { get; init; }

        public IOpponentHand Hand { get; init; }

        public OpponentState(IOpponentHand hand)
        {
            Hand = hand;
        }

        public int StackCount { get; init; }
    }
}
