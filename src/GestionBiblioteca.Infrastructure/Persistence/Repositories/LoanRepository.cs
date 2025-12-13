using GestionBiblioteca.Domain.Entities;
using GestionBiblioteca.Domain.Ports.Out;
using GestionBiblioteca.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBiblioteca.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : Repository<Loan>, ILoanRepository
    {
        public LoanRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
        {
            return await _dbSet
                .Include(l => l.Book) 
                .Where(l => l.Status == "Active")
                .ToListAsync();
        }

        // CORRECCIÓN: Método GetLoansByBookIdAsync añadido
        public async Task<IEnumerable<Loan>> GetLoansByBookIdAsync(int bookId)
        {
            return await _dbSet
                .Where(l => l.BookId == bookId)
                .Include(l => l.Book) 
                .ToListAsync();
        }

        public async Task<Loan?> GetLoanWithBookAsync(int loanId)
        {
            return await _dbSet
                .Include(l => l.Book) 
                .FirstOrDefaultAsync(l => l.Id == loanId);
        }
    }
}