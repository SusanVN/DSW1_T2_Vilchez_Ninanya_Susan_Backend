using Microsoft.AspNetCore.Mvc;
using GestionBiblioteca.Application.DTOs;
using GestionBiblioteca.Application.Interfaces;
using GestionBiblioteca.Domain.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionBiblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Ruta: api/Loan
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // GET: api/Loan/Active (Obtiene todos los préstamos que aún no han sido devueltos)
        [HttpGet("Active")]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetActiveLoans()
        {
            var loans = await _loanService.GetActiveLoansAsync();
            return Ok(loans);
        }

        // POST: api/Loan (Registrar un nuevo préstamo)
        [HttpPost]
        public async Task<ActionResult<LoanDto>> Create([FromBody] CreateLoanDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdLoan = await _loanService.CreateLoanAsync(dto);
                // Retorna 201 Created
                // Usamos GetActiveLoans como referencia de retorno, aunque no haya un GetById explícito.
                return CreatedAtAction(nameof(GetActiveLoans), createdLoan);
            }
            catch (NotFoundException ex)
            {
                // Captura si el BookId no existe (similar a NotFoundException en el POST de Pet)
                return NotFound(new { message = ex.Message });
            }
            catch (BusinessRuleException ex)
            {
                // Captura si el stock es 0 (Regla de negocio)
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Loan/5/Return (Marcar un préstamo como devuelto - Similar al Update/Delete)
        [HttpPut("{id}/Return")]
        public async Task<ActionResult> Return(int id)
        {
            try
            {
                // El servicio se encarga de la lógica de devolución y la transacción
                await _loanService.ReturnLoanAsync(id);
                
                // Retorna 204 No Content
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                // Captura si el LoanId no existe
                return NotFound(new { message = ex.Message });
            }
            catch (BusinessRuleException ex)
            {
                // Captura si el préstamo ya fue devuelto o alguna otra regla
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}