using Microsoft.Extensions.DependencyInjection;
using GestionBiblioteca.Application.Interfaces;
using GestionBiblioteca.Application.Services;
using GestionBiblioteca.Application.Mappers; 
using System.Reflection;

namespace GestionBiblioteca.Application
{
    public static class DependencyInjection
    {
       
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            
            services.AddAutoMapper(typeof(MappingProfile).Assembly); 
            
            
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILoanService, LoanService>();
            
            
            
            return services;
        }
    }
}