using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace UPA.Web
{
    public class ChatHub:Hub
    {
        public void NewMessage(string message)
        {

           
        }
        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("New User", Context.ConnectionId);
          
            return base.OnConnectedAsync();
        }
    }
}
