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
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Book 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // GET: api/Book/id 
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                    return NotFound(new { message = $"Libro con ID {id} no encontrado." });

                return Ok(book);
            }
            catch (NotFoundException ex)
            {
                 
                return NotFound(new { message = ex.Message });
            }
        }
        
        // GET: api/Book/isbn/{isbn}
        [HttpGet("isbn/{isbn}")]
        public async Task<ActionResult<BookDto>> GetByISBN(string isbn)
        {
            var book = await _bookService.GetBookByISBNAsync(isbn);
            if (book == null)
                return NotFound(new { message = $"Libro con ISBN {isbn} no encontrado." });
                
            return Ok(book);
        }

        // POST: api/Book 
        [HttpPost]
        public async Task<ActionResult<BookDto>> Create([FromBody] CreateBookDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdBook = await _bookService.CreateBookAsync(dto);
                
                return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
            }
            catch (DuplicateEntityException ex)
            {
                
                return BadRequest(new { message = ex.Message }); 
            }
            catch (BusinessRuleException ex)
            {
                
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Book/5 
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CreateBookDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _bookService.UpdateBookAsync(id, dto);
                
                return NoContent(); 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (DuplicateEntityException ex)
            {
                 
                return BadRequest(new { message = ex.Message });
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Book/5 
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var success = await _bookService.DeleteBookAsync(id);
                if (!success)
                {
                    
                    return NotFound(new { message = $"Libro con ID {id} no encontrado." });
                }
                
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