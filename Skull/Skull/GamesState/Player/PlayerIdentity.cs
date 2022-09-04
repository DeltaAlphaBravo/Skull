namespace Skull.Skull.GamesState.Player;

internal class PlayerIdentity : IPlayerIdentity
{
    public string Name { get; init; }

    public PlayerIdentity(string name)
    {
        Name = name;
    }
}
