using Skull.GamesState;
using System.Threading.Tasks;

namespace Skull.Tests
{
    public class SingleGameInMemoryGameStateRepository : IGameStateRepository
    {
        private IGameState? _game;

        public async Task<IGameState?> GetGameStateAsync(string key) => await Task.FromResult(_game);

        public async Task SaveGameStateAsync(string key, IGameState gameState) => await Task.FromResult(_game = gameState);
    }
}
