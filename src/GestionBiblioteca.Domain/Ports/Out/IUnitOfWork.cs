using GestionBiblioteca.Domain.Entities;

namespace GestionBiblioteca.Domain.Ports.Out
{
    // Nombre del archivo: IUnitOfWork.cs
    public interface IUnitOfWork : IDisposable
    {
        // Propiedades para acceder a los repositorios
        IBookRepository Books { get; }
        ILoanRepository Loans { get; }

        // Método para guardar todos los cambios
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // Métodos de Transacción (siguiendo el estilo de tu profesor)
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}