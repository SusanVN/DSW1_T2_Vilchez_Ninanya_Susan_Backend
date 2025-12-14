using GestionBiblioteca.Application.DTOs;

namespace GestionBiblioteca.Application.Interfaces
{
    
    public interface IBookService
    {
        
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        
        
        Task<BookDto> CreateBookAsync(CreateBookDto dto);
        
        
        Task<bool> UpdateBookAsync(int id, CreateBookDto dto);
        
        
        Task<bool> DeleteBookAsync(int id);

        Task<BookDto?> GetBookByISBNAsync(string isbn);
    }
}