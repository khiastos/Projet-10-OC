using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using AuthService.Seeding;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace AuthService.Authentification
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder)
            : base(options, logger, encoder)
        {
        }

        private readonly PasswordHasher<IdentityUser> _hasher = new();

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Vérifie la présence du header Authorization
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
            }

            // Header = info transmise dans la requête HTTP (ex : authentification ici)
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            // Vérifie le schéma d'authentification
            if (!"Basic".Equals(authHeader.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Scheme"));
            }

            // On décode en byte le contenu de l'header Authorization car il est encodé en Base64, ça permet d'avoir un texte lisible
            var bytes = Convert.FromBase64String(authHeader.Parameter ?? string.Empty);

            // Credentials = identifiants de connexion username:password
            var credentials = System.Text.Encoding.UTF8.GetString(bytes).Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];

            var user = InMemoryUserSeed.Users
                .FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return Task.FromResult(
                    AuthenticateResult.Fail("User not found")
                );
            }

            // Vérifie le mot de passe entre celui reçu et celui stocké hashé
            var result = _hasher.VerifyHashedPassword(
                user,
                user.PasswordHash!,
                password
            );

            // Pour pouvoir utiliser [Authorize] dans les contrôleurs, on doit créer des claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Ajoute les rôles de l'utilisateur aux claims
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // On crée l'identité avec les claims et le schéma d'authentification
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(
                AuthenticateResult.Success(ticket)
            );

        }

    }
}
