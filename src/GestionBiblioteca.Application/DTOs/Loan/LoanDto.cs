using System;

namespace GestionBiblioteca.Application.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        
        // Campo derivado del Book para mostrar el título en la lista de préstamos
        public string BookTitle { get; set; } = string.Empty;
        
        public string StudentName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        
        // Estado del préstamo (Active, Returned)
        public string Status { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; }
    }
}