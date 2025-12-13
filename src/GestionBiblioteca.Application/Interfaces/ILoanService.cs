using GestionBiblioteca.Application.DTOs;

namespace GestionBiblioteca.Application.Interfaces
{
    // Nombre del archivo: ILoanService.cs
    public interface ILoanService
    {
        // Listar préstamos activos (se pide en el Frontend)
        Task<IEnumerable<LoanDto>> GetActiveLoansAsync();
        
        // Registrar un nuevo préstamo (ejecuta la lógica de negocio de stock--)
        Task<LoanDto> CreateLoanAsync(CreateLoanDto dto); 
        
        // Procesar una devolución (ejecuta la lógica de negocio de stock++)
        Task<bool> ReturnLoanAsync(int loanId); 
    }
}