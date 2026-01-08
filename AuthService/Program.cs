using AuthService.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// EF Core
builder.Services.AddDbContext<AuthDbContext>(opt =>
    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Identity Core + rôles (hash auto, gestion users)
builder.Services.AddIdentityCore<IdentityUser>(i =>
{
    i.Password.RequiredLength = 8;
    i.Password.RequireDigit = true;
    i.Password.RequireNonAlphanumeric = true;
    i.Password.RequireUppercase = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed les rôles et l'admin au démarrage
using (var scope = app.Services.CreateScope())
{
    await IdentitySeeder.SeedAsync(scope.ServiceProvider, app.Configuration);
}
app.Run();
