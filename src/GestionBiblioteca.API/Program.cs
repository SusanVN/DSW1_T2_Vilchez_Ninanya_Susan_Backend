using DotNetEnv;
using GestionBiblioteca.Application;
using GestionBiblioteca.Infrastructure;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// ==========================
// CARGA DE .ENV
// ==========================
var currentDir = Directory.GetCurrentDirectory();
var envPath = Path.Combine(currentDir, "..", "..", ".env");

if (File.Exists(envPath))
{
    DotNetEnv.Env.Load(envPath);
    Console.WriteLine($".env file loaded from: {envPath}");
}
else
{
    Console.WriteLine(".env file not found. Connection may fail.");
}

// ==========================
// SERVICIOS
// ==========================
builder.Services.AddControllers();

// 👉 CORS (NECESARIO PARA FRONTEND)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Arquitectura
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

// ❌ QUITAMOS Swagger
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// ==========================
// PIPELINE
// ==========================
app.UseHttpsRedirection();

// 👉 CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();