using GestionBiblioteca.Domain.Entities;

namespace GestionBiblioteca.Domain.Ports.Out
{
    // Nombre del archivo: IBookRepository.cs
    public interface IBookRepository : IRepository<Book>
    {
       
        Task<Book?> GetByISBNAsync(string isbn);
    }
}