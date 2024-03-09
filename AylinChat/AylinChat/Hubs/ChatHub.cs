using AylinChat.Client.Models;
using AylinChat.Repos;
using ChatModels;
using Microsoft.AspNetCore.SignalR;

namespace AylinChat.Hubs
{
    public class ChatHub(ChatRepo chatRepo) : Hub
    {
        public async Task SendMessage(Chat chat)
        { 
            await chatRepo.SaveChatAsync(chat);
            await Clients.All.SendAsync("ReciveMessage", chat); 
        }
        //model 
        public async Task AddAvailableUserAsync(AvailableUser availableUser)
        {
            availableUser.ConnectionId = Context.ConnectionId;
            await chatRepo.AddAvailableUserAsync(availableUser);
            var users = chatRepo.GetAvailableUsersAsync();
            await Clients.All.SendAsync("NotifyAllClient", users);
        }
    }
}
