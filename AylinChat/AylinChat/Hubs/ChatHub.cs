using AylinChat.Repos;
using ChatModels;
using ChatModels.DTOs;
using ChatModels.Models;
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
            await Clients.All.SendAsync("NotifyAllClient", await GetUsers());
        }
        public async Task RemoveUserAsync(string userId)
        {
            await chatRepo.RemoveUserAsync(userId);
            await Clients.All.SendAsync("NotifyAllClient", await GetUsers());
        }

        private async Task<List<AvailableUserDTO>> GetUsers()
            =>   await chatRepo.GetAvailableUsersAsync();
       
    }
}
