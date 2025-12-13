using GestionBiblioteca.Application.DTOs;

namespace GestionBiblioteca.Application.Interfaces
{
    
    public interface IBookService
    {
        // CRUD Básico
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        
        // Crear (usa CreateBookDto como entrada, devuelve BookDto con Id)
        Task<BookDto> CreateBookAsync(CreateBookDto dto);
        
        // Actualizar (usa CreateBookDto como entrada para los datos a modificar)
        Task<bool> UpdateBookAsync(int id, CreateBookDto dto);
        
        // Eliminar
        Task<bool> DeleteBookAsync(int id);

        Task<BookDto?> GetBookByISBNAsync(string isbn);
    }
}