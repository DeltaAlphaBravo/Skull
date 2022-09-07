namespace Skull.GamesState.Player;

internal class PlayerIdentity : IPlayerIdentity
{
    public string Name { get; private init; }
    public int PlayerId { get; private init; }

    public PlayerIdentity(string name, int id)
    {
        Name = name;
        PlayerId = id;
    }
}
