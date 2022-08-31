using Skull.GamesState;

namespace Skull.Phases
{
    internal interface ICreationPhase
    {
        IGameState GameState { get; }
        IGameState JoinPlayer(string name);
    }
}