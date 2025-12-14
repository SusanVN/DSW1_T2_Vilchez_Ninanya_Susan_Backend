using DotNetEnv;
using GestionBiblioteca.Application;
using GestionBiblioteca.Infrastructure;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);


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


builder.Services.AddControllers();


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


builder.Services.AddInfrastructure();
builder.Services.AddApplication();


var app = builder.Build();


app.UseHttpsRedirection();


app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();