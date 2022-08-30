namespace Skull.GamesState;

internal sealed class Hand : IHand
{
    public int PlayerId { get; init; }

    public bool HasSkull { get; set; }

    public int CardCount { get; set; }
}
