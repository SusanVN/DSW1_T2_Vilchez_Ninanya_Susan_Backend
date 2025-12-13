namespace GestionBiblioteca.Application.DTOs
{
    public class CreateLoanDto
    {
        // Identificador del libro que se prestará (FK a Books)
        public int BookId { get; set; }
        
        // Nombre del estudiante que solicita el préstamo (varchar(150))
        public string StudentName { get; set; } = string.Empty;
    }
}