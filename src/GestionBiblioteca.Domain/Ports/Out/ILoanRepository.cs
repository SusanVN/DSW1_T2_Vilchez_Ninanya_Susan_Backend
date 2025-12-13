using GestionBiblioteca.Domain.Entities;

namespace GestionBiblioteca.Domain.Ports.Out
{
    // Nombre del archivo: ILoanRepository.cs
    public interface ILoanRepository : IRepository<Loan>
    {
        // Métodos específicos útiles para el negocio de préstamos y devoluciones
        Task<IEnumerable<Loan>> GetActiveLoansAsync();
        Task<IEnumerable<Loan>> GetLoansByBookIdAsync(int bookId);
        Task<Loan?> GetLoanWithBookAsync(int id);
    }
}