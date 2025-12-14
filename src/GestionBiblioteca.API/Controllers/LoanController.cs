using Microsoft.AspNetCore.Mvc;
using GestionBiblioteca.Application.DTOs;
using GestionBiblioteca.Application.Interfaces;
using GestionBiblioteca.Domain.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionBiblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // GET: api/Loan/Active 
        [HttpGet("Active")]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetActiveLoans()
        {
            var loans = await _loanService.GetActiveLoansAsync();
            return Ok(loans);
        }

        // POST: api/Loan 
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
                
                return CreatedAtAction(nameof(GetActiveLoans), createdLoan);
            }
            catch (NotFoundException ex)
            {
                
                return NotFound(new { message = ex.Message });
            }
            catch (BusinessRuleException ex)
            {
                
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Loan/5/Return 
        [HttpPut("{id}/Return")]
        public async Task<ActionResult> Return(int id)
        {
            try
            {
                
                await _loanService.ReturnLoanAsync(id);
                
                
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                
                return NotFound(new { message = ex.Message });
            }
            catch (BusinessRuleException ex)
            {
                
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}