using AylinChat.Authentication;
using AylinChat.Data;
using ChatModels;
using ChatModels.DTOs;
using ChatModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AylinChat.Repos
{
    public class ChatRepo(AppDbContext appDbContext, UserManager<AppUser> userManager )
    {
        public async Task SaveChatAsync(Chat chat) 
        {
            appDbContext.Chats.Add(chat);
            await appDbContext.SaveChangesAsync();
        }
        public async Task<List<Chat>> GetChatsAsync() => await appDbContext.Chats.ToListAsync();

        public async Task AddAvailableUserAsync(AvailableUser availableUser)
        {
            var getUser = await appDbContext.AvailableUsers.FirstOrDefaultAsync
                ( _=>_.UserId == availableUser.UserId);

            if (getUser != null) getUser.ConnectionId = availableUser.ConnectionId;
            else appDbContext.AvailableUsers.Add(availableUser);
            
            await appDbContext.SaveChangesAsync();
        }
        public async Task<List<AvailableUserDTO>> GetAvailableUsersAsync()
        {
            var list = new List<AvailableUserDTO>();
            var users = await appDbContext.AvailableUsers.ToListAsync();
            foreach (var u in users)
            {
                list.Add(new AvailableUserDTO(UserId: u.UserId, ConnectionId: u.ConnectionId , 
                    Fullname: ( await userManager.FindByIdAsync(u.UserId!)!)!.FullName,
                    Email: ( await userManager.FindByIdAsync(u.UserId!)!)!.Email!    
                    ));
            }
            return list;
        }
        public async Task RemoveUserAsync(string userId)
        {
            var user = await appDbContext.AvailableUsers.FirstOrDefaultAsync ( u => u.UserId == userId );
                if (user != null)
            {
                appDbContext.AvailableUsers.Remove(user);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}
