using Skull.GamesState;

namespace Skull.Api
{
    public class SingleTableInMemoryTableRepository : ITableRepository
    {
        private ITable? _table;

        public async Task<ITable?> GetTableAsync(string name) 
        {
            if(name == _table?.Name) return await Task.FromResult(_table);
            return null;
        }

        public async Task SaveTableAsync(ITable table) =>  await Task.FromResult(_table = table);
    }
}