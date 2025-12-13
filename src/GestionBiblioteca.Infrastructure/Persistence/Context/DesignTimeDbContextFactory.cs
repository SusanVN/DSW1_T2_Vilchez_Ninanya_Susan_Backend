using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;

// Asegúrate de usar el namespace correcto
namespace GestionBiblioteca.Infrastructure.Persistence.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // La lógica del profesor para cargar el .env
            var envPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".env");
            if (File.Exists(envPath))
            {
                var lines = File.ReadAllLines(envPath);
                foreach (var line in lines)
                {
                    var parts = line.Split('=', 2);
                    if (parts.Length == 2)
                    {
                        // Nota: Tu .env usa DB_SERVER, DB_PORT, etc.
                        Environment.SetEnvironmentVariable(parts[0], parts[1]);
                    }
                }
            }

            // Usamos tus variables de entorno, que deberían estar cargadas por el código de arriba
            var host = Environment.GetEnvironmentVariable("DB_SERVER"); // Nota: Usar DB_SERVER
            var port = Environment.GetEnvironmentVariable("DB_PORT");
            var database = Environment.GetEnvironmentVariable("DB_NAME");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            var connectionString = $"Server={host};Port={port};Database={database};User={user};Password={password};";

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            // Usar la versión 8.0.0 que definió tu profesor en DependencyInjection.cs
            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 0))
            );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}