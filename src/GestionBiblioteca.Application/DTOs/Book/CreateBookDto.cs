namespace GestionBiblioteca.Application.DTOs
{
    public class CreateBookDto
    {
        // Título del libro (varchar(200))
        public string Title { get; set; } = string.Empty;
        
        // Autor del libro (varchar(150))
        public string Author { get; set; } = string.Empty;
        
        // Código ISBN (varchar(20), debe ser único)
        public string ISBN { get; set; } = string.Empty;
        
        // Cantidad inicial o stock a actualizar (int)
        public int Stock { get; set; }
    }
}