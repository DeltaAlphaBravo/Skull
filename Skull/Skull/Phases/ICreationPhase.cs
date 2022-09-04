using Skull.Skull.GamesState;

namespace Skull.Skull.Phases
{
    internal interface ICreationPhase
    {
        IGameState GameState { get; }
        int JoinPlayer(string name);
        IGameState PlaceFirstCoaster(int player, bool isSkull);
    }
}