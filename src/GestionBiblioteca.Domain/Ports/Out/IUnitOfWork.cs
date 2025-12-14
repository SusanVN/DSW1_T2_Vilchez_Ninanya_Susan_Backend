using GestionBiblioteca.Domain.Entities;

namespace GestionBiblioteca.Domain.Ports.Out
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        ILoanRepository Loans { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}