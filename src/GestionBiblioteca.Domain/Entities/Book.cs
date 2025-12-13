namespace GestionBiblioteca.Domain.Entities
{
    public class Book
    {
        
        public int Id { get; set; } // PK [cite: 35]
        public string Title { get; set; } = string.Empty; // varchar(200) [cite: 35]
        public string Author { get; set; } = string.Empty; // varchar(150) [cite: 35]
        public string ISBN { get; set; } = string.Empty; // varchar(20) (único) [cite: 35, 41]
        public int Stock { get; set; } // int [cite: 35]
        public DateTime CreatedAt { get; set; } = DateTime.Now; // datetime [cite: 35]
        
        // Propiedad de Navegación (Colección de préstamos asociados a este libro)
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        
        // Lógica de Dominio (Ejemplo)
        public bool IsAvailable() => Stock > 0; // Se pide en la regla de negocio [cite: 39]
    }
}