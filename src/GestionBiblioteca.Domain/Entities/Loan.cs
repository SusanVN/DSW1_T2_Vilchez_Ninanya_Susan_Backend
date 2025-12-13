namespace GestionBiblioteca.Domain.Entities
{
    public class Loan
    {
        
        public int Id { get; set; } // PK 
        public string StudentName { get; set; } = string.Empty; // varchar(150) 
        public DateTime LoanDate { get; set; } = DateTime.Now; // datetime 
        public DateTime? ReturnDate { get; set; } // datetime (null) 
        public string Status { get; set; } = "Active"; // varchar(20) (Active, Returned) [cite: 37]
        public DateTime CreatedAt { get; set; } = DateTime.Now; // datetime 
        
        // Clave Foránea
        public int BookId { get; set; } // int (FK) 
        
        // Propiedad de Navegación (Referencia al libro prestado)
        public Book? Book { get; set; }
        
        // Lógica de Dominio (Ejemplo)
        public void MarkAsReturned()
        {
            if (Status == "Active")
            {
                Status = "Returned";
                ReturnDate = DateTime.Now;
            }
        }
    }
}