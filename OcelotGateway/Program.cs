using System.Text;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
    throw new Exception("JWT KEY IS MISSING IN OCELOT");

builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "Medilabo",

            ValidateAudience = true,
            ValidAudience = "MedilaboClient",

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

            RoleClaimType =
                "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };
    });


builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();
app.UseOcelot().Wait();

app.Run();
