using SkullApi.Models.Internal.GamesState;

namespace SkullApi
{
    public interface IGameStateRepository
    {
        Task<IGameState?> GetGameStateAsync(string name);
        Task SaveGameStatusAsync(IGameState gameState);
    }
}
