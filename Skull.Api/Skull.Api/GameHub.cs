using Microsoft.AspNetCore.SignalR;

namespace Skull.Api
{
    public class GameHub : Hub
    {
        public async Task AddToGroupAsync(string game)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, game);
        }

        public async Task SendMessageAsync(string game, string message)
        {
            await Clients.Group("Booyah").SendAsync("ReceiveMessage", message);
        }
    }
}
