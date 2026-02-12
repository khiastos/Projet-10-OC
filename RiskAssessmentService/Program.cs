using System.Text;
using Microsoft.IdentityModel.Tokens;
using RiskAssessmentService.Clients;
using RiskAssessmentService.Clients.Interfaces;
using RiskAssessmentService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();

// Clients HTTP
builder.Services.AddScoped<IRiskAssessmentService, RiskAssessmentService.Services.RiskAssessmentService>();

builder.Services.AddHttpClient<IPatientsClient, PatientsClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:PatientsService"]!);
});

builder.Services.AddHttpClient<INotesClient, NotesClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:NotesService"]!);
});

builder.Services.AddOpenApi();

// JWT 
builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "Medilabo",

            ValidateAudience = true,
            ValidAudience = "MedilaboClient",

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            ),

            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
