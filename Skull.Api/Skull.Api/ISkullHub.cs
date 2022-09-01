
namespace Skull.Api
{
    public interface ISkullHub
    {
        Task AddToGroupAsync(string game);
        Task SendMessageAsync(string game, string message);
    }
}