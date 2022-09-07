using Skull.GamesState;

namespace Skull;

public interface IGameStateRepository
{
    Task<IGameState?> GetGameStateAsync(string key);
    Task SaveGameStateAsync(string key, IGameState gameState);
}
