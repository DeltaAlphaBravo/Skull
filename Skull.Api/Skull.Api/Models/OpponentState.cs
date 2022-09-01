namespace Skull.Api.Models
{
    public sealed class OpponentState : IOpponentState
    {
        public int PlayerId { get; init; }

        public IOpponentHand Hand { get; init; }

        public OpponentState(int playerId, string name, IOpponentHand hand)
        {
            PlayerId = playerId;
            Name = name;
            Hand = hand;
        }

        public int StackCount { get; init; }

        public string Name {get; init; }
    }
}
