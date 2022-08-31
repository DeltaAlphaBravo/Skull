using Microsoft.AspNetCore.SignalR;

namespace Skull.Api
{
    public class GameHub : Hub
    {
        public async Task SendMessage(string game, string user, string message)
        {
            await Clients.Clients(new[] { "Booyah" }).SendAsync("ReceiveMessage", message);
        }
    }
}
