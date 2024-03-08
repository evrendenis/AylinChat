using AylinChat.Authentication;
using ChatModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AylinChat.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {           
        }
        public DbSet<Chat> Chats { get; set; }
    }   
}
