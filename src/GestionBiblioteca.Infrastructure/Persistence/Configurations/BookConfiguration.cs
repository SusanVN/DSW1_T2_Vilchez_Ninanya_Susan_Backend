using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestionBiblioteca.Domain.Entities;

namespace GestionBiblioteca.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // Mapeo a la tabla Books
            builder.ToTable("Books");

            // Clave Primaria
            builder.HasKey(b => b.Id);

            // Propiedades escalares y restricciones
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200); // varchar(200) [cite: 35]

            builder.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(150); // varchar(150) [cite: 35]

            builder.Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(20); // varchar(20) [cite: 35]

            builder.Property(b => b.Stock)
                .IsRequired(); // int [cite: 35]

            builder.Property(b => b.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime"); // datetime [cite: 35]

            
            builder.HasIndex(b => b.ISBN).IsUnique();

            // Relación: Book (1) a Loans (M)
            builder.HasMany(b => b.Loans)
                .WithOne(l => l.Book)
                .HasForeignKey(l => l.BookId)
                // Se recomienda Restrict para evitar eliminar libros con préstamos activos
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}