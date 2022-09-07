namespace Skull;

public interface ITableRepository
{
    Task<ITable> GetTableAsync(string name);
    Task SaveTableAsync(ITable table);
}
