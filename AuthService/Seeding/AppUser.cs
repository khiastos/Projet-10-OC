using Microsoft.AspNetCore.Identity;

namespace AuthService.Seeding
{
    public class AppUser : IdentityUser
    {
        public List<String> Roles { get; set; } = new();
    }
}
