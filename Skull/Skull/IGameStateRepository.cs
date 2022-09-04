using Skull.Skull.GamesState;

namespace Skull.Skull
{
    public interface IGameStateRepository
    {
        Task<IGameState?> GetGameStateAsync(string name);
        Task SaveGameStatusAsync(IGameState gameState);
    }
}
