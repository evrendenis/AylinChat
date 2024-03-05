using AylinChat.Data;
using ChatModels;
using Microsoft.EntityFrameworkCore;

namespace AylinChat.Repos
{
    public class ChatRepo(AppDbContext appDbContext)
    {
        public async Task SaveChatAsync(Chat chat) 
        {
            appDbContext.Chats.Add(chat);
            await appDbContext.SaveChangesAsync();
        }
        public async Task<List<Chat>> GetChatsAsync() => await appDbContext.Chats.ToListAsync();
    }
}
