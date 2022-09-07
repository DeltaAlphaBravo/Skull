using Skull.GamesState.GamePlay;

namespace Skull.GamesState
{
    internal class Bid : IBid
    {
        public int PlayerId { get; init; }
        public int? CardsToReveal { get; init; }
    }
}
