using Microsoft.Extensions.DependencyInjection;
using GestionBiblioteca.Application.Interfaces;
using GestionBiblioteca.Application.Services;
using GestionBiblioteca.Application.Mappers; // Asegúrate de que esta sea la ruta a tu MappingProfile
using System.Reflection;

namespace GestionBiblioteca.Application
{
    public static class DependencyInjection
    {
        // Método de extensión para inyectar los servicios de la capa Application
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // 1. Registro de AutoMapper
            // Usamos AddAutoMapper y le indicamos que escanee el ensamblado donde está MappingProfile.
            // Nota: Usar typeof(MappingProfile).Assembly es igual de válido que Assembly.GetExecutingAssembly() si están en el mismo proyecto.
            services.AddAutoMapper(typeof(MappingProfile).Assembly); 
            
            // 2. Registro de Servicios (Mapeando Interfaces a Implementaciones)
            // Adaptado a tus interfaces IBookService e ILoanService
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILoanService, LoanService>();
            
            // Si tuvieras excepciones (como en el ejemplo de tu profesor), 
            // la lógica de gestión de excepciones se registra aquí o en la API.
            
            return services;
        }
    }
}