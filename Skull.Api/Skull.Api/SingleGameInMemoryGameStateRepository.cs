using SkullApi.Models.Internal.GamesState;

namespace SkullApi
{
    public class SingleGameInMemoryGameStateRepository : IGameStateRepository
    {
        private IGameState? _game;

        public async Task<IGameState?> GetGameStateAsync(string name) => await Task.FromResult(_game);

        public async Task SaveGameStatusAsync(IGameState gameState) => await Task.FromResult(_game = gameState);
    }
}
