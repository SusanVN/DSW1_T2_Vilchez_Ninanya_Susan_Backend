namespace GestionBiblioteca.Domain.Entities
{
    public class Loan
    {
        
        public int Id { get; set; } 
        public string StudentName { get; set; } = string.Empty; 
        public DateTime LoanDate { get; set; } = DateTime.Now; 
        public DateTime? ReturnDate { get; set; } 
        public string Status { get; set; } = "Active"; 
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
        
       
        public int BookId { get; set; } 
        
        
        public Book? Book { get; set; }
        
       
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