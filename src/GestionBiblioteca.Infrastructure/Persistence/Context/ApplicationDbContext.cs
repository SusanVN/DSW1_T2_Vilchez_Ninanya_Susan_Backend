using Microsoft.EntityFrameworkCore;
using GestionBiblioteca.Domain.Entities;
using GestionBiblioteca.Infrastructure.Persistence.Configurations;
using System.Reflection;

namespace GestionBiblioteca.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Definición de los DbSet para las entidades
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar las configuraciones de mapeo explícitamente, 
            // siguiendo el estilo de tu profesor:
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new LoanConfiguration());

            // Nota: La configuración de la relación de entidades
            // (1:N entre Book y Loan) ya se define dentro de BookConfiguration y LoanConfiguration.
        }
    }
}