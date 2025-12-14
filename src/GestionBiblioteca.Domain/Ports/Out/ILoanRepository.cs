using GestionBiblioteca.Domain.Entities;

namespace GestionBiblioteca.Domain.Ports.Out
{
    
    public interface ILoanRepository : IRepository<Loan>
    {
        
        Task<IEnumerable<Loan>> GetActiveLoansAsync();
        Task<IEnumerable<Loan>> GetLoansByBookIdAsync(int bookId);
        Task<Loan?> GetLoanWithBookAsync(int id);
    }
}