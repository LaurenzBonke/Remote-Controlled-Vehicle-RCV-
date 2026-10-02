using Microsoft.EntityFrameworkCore;
using pigames.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Zugangsdaten der Datenbank stehen nicht im Repo: lokal per "dotnet user-secrets", sonst per
// Umgebungsvariable ConnectionStrings__DefaultConnection (siehe README.md)
builder.Services.AddDbContext<PigamesosContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' fehlt. Lokal: dotnet user-secrets set \"ConnectionStrings:DefaultConnection\" \"<Verbindung>\", " +
        "sonst Umgebungsvariable ConnectionStrings__DefaultConnection setzen.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
