namespace SkullApi.Models.Internal.GamesState
{
    public interface IPlayer : IPlayerState
    {
        public IPlayerState PlaySkull();
        public IPlayerState PlayFlower();
    }
}
