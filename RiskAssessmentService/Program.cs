using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
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
