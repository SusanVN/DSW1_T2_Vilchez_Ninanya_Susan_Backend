using GestionBiblioteca.Application.DTOs;

namespace GestionBiblioteca.Application.Interfaces
{
    
    public interface ILoanService
    {
        
        Task<IEnumerable<LoanDto>> GetActiveLoansAsync();
        
       
        Task<LoanDto> CreateLoanAsync(CreateLoanDto dto); 
        
        
        Task<bool> ReturnLoanAsync(int loanId); 
    }
}