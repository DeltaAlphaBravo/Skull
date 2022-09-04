namespace Skull.Skull.GamesState.Player;

internal class Player : IPlayer
{
    public int PlayerId { get; private init; }
    public IPlayerState PlayerState { get; private set; }
    public IPlayerIdentity? PlayerIdentity { get; private set; }
    public Player(int playerId)
    {
        PlayerId = playerId;
        PlayerState = new PlayerState(playerId);
    }

    public IPlayer AttachIdentity(string name)
    {
        PlayerIdentity = new PlayerIdentity(name);
        return this;
    }

    public IPlayerState PlaySkull()
    {
        if (!PlayerState.Hand.HasSkull) throw new InvalidOperationException($"Player {PlayerId} tried to play a skull, but has no skull");
        PlayerState.Hand.HasSkull = false;

        return PlayCard(Coaster.Skull);
    }

    public IPlayerState PlayFlower()
    {
        if (PlayerState.Hand.HasSkull && PlayerState.Hand.CardCount == 1)
            throw new InvalidOperationException($"Player {PlayerId} tried to play a flower, but has no flower");
        if (PlayerState.Hand.CardCount < 1)
            throw new InvalidOperationException($"Player {PlayerId} tried to play a flower, but has no cards");
        return PlayCard(Coaster.Flower);
    }

    private IPlayerState PlayCard(Coaster coaster)
    {
        PlayerState.PlayedCoasters.Push(coaster);
        PlayerState.Hand.CardCount--;
        return PlayerState;
    }
}
