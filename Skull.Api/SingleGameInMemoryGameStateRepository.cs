using Skull.GamesState;

namespace Skull.Api
{
    public class SingleGameInMemoryGameStateRepository : IGameStateRepository
    {
        private IGameState? _game;

        public async Task<IGameState?> GetGameStateAsync(string name) => await Task.FromResult(_game);

        public async Task SaveGameStateAsync(string name, IGameState gameState) =>  await Task.FromResult(_game = gameState);
    }
}