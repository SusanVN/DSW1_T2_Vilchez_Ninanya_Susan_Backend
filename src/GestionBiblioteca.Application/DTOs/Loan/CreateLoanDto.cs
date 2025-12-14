namespace GestionBiblioteca.Application.DTOs
{
    public class CreateLoanDto
    {
        
        public int BookId { get; set; }
        
        
        public string StudentName { get; set; } = string.Empty;
    }
}