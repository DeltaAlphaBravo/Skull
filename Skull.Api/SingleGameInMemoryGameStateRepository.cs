using Skull.GamesState;

namespace Skull.Api
{
    public class SingleGameInMemoryGameStateRepository : IGameStateRepository
    {
        private IGameState? _game;
        private string? _gameName;

        public async Task<IGameState?> GetGameStateAsync(string name) => await Task.FromResult(_gameName == name ? _game : null);

        public async Task SaveGameStateAsync(string name, IGameState gameState)
        {
            _game = gameState;
            _gameName = name;
            await Task.FromResult(true);
        }
    }
}