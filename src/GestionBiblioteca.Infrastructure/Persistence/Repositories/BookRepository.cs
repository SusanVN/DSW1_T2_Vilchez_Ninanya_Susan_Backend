using GestionBiblioteca.Domain.Entities;
using GestionBiblioteca.Domain.Ports.Out;
using GestionBiblioteca.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestionBiblioteca.Infrastructure.Persistence.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Método específico para validar la unicidad del ISBN
        public async Task<Book?> GetByISBNAsync(string isbn)
        {
            return await _dbSet
                .FirstOrDefaultAsync(b => b.ISBN == isbn);
        }
    }
}