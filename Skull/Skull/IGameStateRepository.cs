using Skull.GamesState;

namespace Skull
{
    public interface IGameStateRepository
    {
        Task<IGameState?> GetGameStateAsync(string name);
        Task SaveGameStatusAsync(IGameState gameState);
    }
}
