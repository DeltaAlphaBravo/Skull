namespace Skull.GamesState
{
    internal class RevealedCoaster : IRevealedCoaster
    {
        public int PlayerId { get; init; }

        public bool IsSkull { get; init; }
    }
}
