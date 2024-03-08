using Microsoft.AspNetCore.Identity;

namespace AylinChat.Authentication
{
    public class AppUser : IdentityUser
    {     

        public string FullName { get; set; } = string.Empty;
    }
}

