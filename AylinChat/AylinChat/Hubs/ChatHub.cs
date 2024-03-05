using ChatModels;
using Microsoft.AspNetCore.SignalR;

namespace AylinChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Chat chat) 
        => await Clients.All.SendAsync("ReciveMessage", chat );        
    }
}
