using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestionBiblioteca.Domain.Entities;

namespace GestionBiblioteca.Infrastructure.Persistence.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            // Mapeo a la tabla Loans
            builder.ToTable("Loans");

            // Clave Primaria
            builder.HasKey(l => l.Id);

            // Clave Foránea
            builder.Property(l => l.BookId)
                .IsRequired(); // int (FK) [cite: 37]

            // Propiedades escalares y restricciones
            builder.Property(l => l.StudentName)
                .IsRequired()
                .HasMaxLength(150); // varchar(150) [cite: 37]

            builder.Property(l => l.LoanDate)
                .IsRequired()
                .HasColumnType("datetime"); // datetime [cite: 37]

            builder.Property(l => l.ReturnDate)
                .HasColumnType("datetime")
                .IsRequired(false); // datetime (null) [cite: 37]

            builder.Property(l => l.Status)
                .IsRequired()
                .HasMaxLength(20); // varchar(20) (e.g., Active, Returned) [cite: 37]

            builder.Property(l => l.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime"); // datetime [cite: 37]

            // Índice sobre el BookId (opcional, pero mejora rendimiento)
            builder.HasIndex(l => l.BookId);

            // Relación: Loan (M) a Book (1)
            builder.HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId);
        }
    }
}