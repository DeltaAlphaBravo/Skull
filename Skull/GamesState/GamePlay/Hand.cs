namespace Skull.GamesState.GamePlay;

internal sealed class Hand : IHand
{
    public bool HasSkull { get; set; }

    public int CardCount { get; set; }
}
