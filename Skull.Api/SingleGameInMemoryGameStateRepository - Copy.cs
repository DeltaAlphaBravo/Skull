using Skull.GamesState;

namespace Skull.Api
{
    public class SingleTableInMemoryTableRepository : ITableRepository
    {
        private ITable? _table;

        public async Task<ITable?> GetTableAsync(string name) => await Task.FromResult(_table);

        public async Task SaveTableAsync(ITable table) =>  await Task.FromResult(_table = table);
    }
}