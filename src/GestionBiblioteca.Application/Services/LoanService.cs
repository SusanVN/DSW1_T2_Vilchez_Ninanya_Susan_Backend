using AutoMapper;
using GestionBiblioteca.Application.DTOs;
using GestionBiblioteca.Application.Interfaces;
using GestionBiblioteca.Domain.Entities;
using GestionBiblioteca.Domain.Exceptions;
using GestionBiblioteca.Domain.Ports.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBiblioteca.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoanDto>> GetActiveLoansAsync()
        {
            var loans = await _unitOfWork.Loans.GetActiveLoansAsync();
            var dtos = _mapper.Map<IEnumerable<LoanDto>>(loans);
            
            // Asignar títulos para el DTO, asumiendo que el repositorio incluyó el Book
            var loanList = loans.ToList();
            var dtoList = dtos.ToList();

            for (int i = 0; i < dtoList.Count; i++)
            {
                dtoList[i].BookTitle = loanList[i].Book?.Title ?? "N/A";
            }
            return dtoList;
        }

        public async Task<LoanDto> CreateLoanAsync(CreateLoanDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try {
                var book = await _unitOfWork.Books.GetByIdAsync(dto.BookId);
                if (book == null) throw new NotFoundException("Libro", dto.BookId);

                // Regla de Negocio: No se puede prestar si el Stock es 0
                if (book.Stock <= 0) throw new BusinessRuleException("Stock", "No hay stock disponible para el libro solicitado.");

                // Regla de Negocio: Al registrar, el Stock disminuye en 1
                book.Stock--;
                await _unitOfWork.Books.UpdateAsync(book);

                var loan = _mapper.Map<Loan>(dto);
                loan.Status = "Active";
                loan.LoanDate = DateTime.Now;
                loan.CreatedAt = DateTime.Now;
                
                var created = await _unitOfWork.Loans.AddAsync(loan);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                
                var result = _mapper.Map<LoanDto>(created);
                result.BookTitle = book.Title;
                return result;
            } catch {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> ReturnLoanAsync(int loanId)
        {
            await _unitOfWork.BeginTransactionAsync();
            try {
                var loan = await _unitOfWork.Loans.GetLoanWithBookAsync(loanId);
                if (loan == null) throw new NotFoundException("Préstamo", loanId);
                if (loan.Status == "Returned") throw new BusinessRuleException("Estado", "El préstamo ya fue devuelto.");
                if (loan.Book == null) throw new NotFoundException("Libro Asociado al Préstamo", loan.BookId);


                // Aplicar lógica de dominio: Marcar el préstamo como devuelto
                loan.MarkAsReturned();
                await _unitOfWork.Loans.UpdateAsync(loan);

                // Regla de Negocio: Al devolver, el Stock aumenta en 1
                loan.Book.Stock++;
                await _unitOfWork.Books.UpdateAsync(loan.Book);
                
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return true;
            } catch {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}