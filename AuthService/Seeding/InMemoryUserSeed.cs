using Microsoft.AspNetCore.Identity;

namespace AuthService.Seeding
{
    public class InMemoryUserSeed
    {
        public static List<IdentityUser> Users = new();

        // Créé un utilisateur au lancement de l'application
        static InMemoryUserSeed()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var user = new IdentityUser
            {
                UserName = "admin",
                Email = "admin@gmail.com"
            };

            user.PasswordHash = hasher.HashPassword(user, "Password123!");

            Users.Add(user);
        }
    }
}
