using Microsoft.AspNetCore.SignalR;

namespace UPA.Web.Controllers
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

    }
}