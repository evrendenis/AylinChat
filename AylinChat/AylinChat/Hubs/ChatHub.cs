using Microsoft.AspNetCore.SignalR;

namespace AylinChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string userName , string Message, DateTime date) 
        => await Clients.All.SendAsync("ReciveMessage", userName,Message,date);

        
    }
}
