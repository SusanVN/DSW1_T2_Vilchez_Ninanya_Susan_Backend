using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GestionBiblioteca.Domain.Ports.Out;
using GestionBiblioteca.Infrastructure.Persistence.Context;
using GestionBiblioteca.Infrastructure.Persistence.Repositories;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // Para UseMySql
using System;
using System.Reflection;

namespace GestionBiblioteca.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // 1. Obtención de Variables de Entorno (Igual que tu profesor)
        var host = Environment.GetEnvironmentVariable("DB_SERVER");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var database = Environment.GetEnvironmentVariable("DB_NAME");
        var user = Environment.GetEnvironmentVariable("DB_USER");
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

        var connectionString = $"Server={host};Port={port};Database={database};User={user};Password={password};";

        // 2. Comprobar si las variables de entorno se cargaron correctamente
        // Si las variables de entorno no están definidas (lo que pasa durante la migración o ejecución local sin .env)
        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(database))
        {
             // FALLBACK: Usamos la cadena de conexión harcodeada o leída de appsettings
             // Reemplaza esta línea con tu cadena de conexión local real
             connectionString = "Server=localhost;Port=3306;Database=biblioteca_db;Uid=root;Pwd=admin;";
             Console.WriteLine("ADVERTENCIA: Usando cadena de conexión local (FALLBACK).");
        }
        
        // El profesor usa Console.WriteLine(connectionString) para debug
        Console.WriteLine($"Connection String: {connectionString}");

        // 3. Configurar DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                connectionString,
                // Usaremos la versión de MySQL que tu profesor especificó, que es más segura
                new MySqlServerVersion(new Version(8, 0, 0)) 
            )
        );

        // 4. Registrar Repositorios (Tus entidades)
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}