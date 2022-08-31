namespace Skull.GamesState
{
    public interface IPlayer
    {
        int PlayerId { get; }
        IPlayerState PlayerState { get; }
        IPlayerIdentity? PlayerIdentity { get; }
        IPlayer AttachIdentity(string name);
        IPlayerState PlaySkull();
        IPlayerState PlayFlower();
    }
}
