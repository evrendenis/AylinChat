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
    }
}
