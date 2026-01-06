using Microsoft.AspNetCore.Identity;

namespace AuthService.Seeding
{
    public class InMemoryUserSeed
    {
        public static List<AppUser> Users = new();

        // Créé un utilisateur au lancement de l'application
        static InMemoryUserSeed()
        {
            var hasher = new PasswordHasher<AppUser>();

            var user = new AppUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                Roles = { "Admin" }
            };

            user.PasswordHash = hasher.HashPassword(user, "Password123!");

            Users.Add(user);
        }
    }
}
