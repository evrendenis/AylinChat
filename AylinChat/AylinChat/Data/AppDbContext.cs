using ChatModels;
using Microsoft.EntityFrameworkCore;

namespace AylinChat.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Chat> Chats { get; set; }
    }
}
