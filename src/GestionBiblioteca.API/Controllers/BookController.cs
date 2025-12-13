using Microsoft.AspNetCore.Mvc;
using GestionBiblioteca.Application.DTOs;
using GestionBiblioteca.Application.Interfaces;
using GestionBiblioteca.Domain.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionBiblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Ruta: api/Book
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Book (Similar a GetAll de PetsController)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // GET: api/Book/5 (Similar a GetById de PetsController)
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
                 // Manejo de excepción de dominio, similar a GetByOwnerId con try-catch
                return NotFound(new { message = ex.Message });
            }
        }
        
        // GET: api/Book/isbn/978-0321765723 (Similar a GetBySpecies o GetByOwnerId)
        [HttpGet("isbn/{isbn}")]
        public async Task<ActionResult<BookDto>> GetByISBN(string isbn)
        {
            var book = await _bookService.GetBookByISBNAsync(isbn);
            if (book == null)
                return NotFound(new { message = $"Libro con ISBN {isbn} no encontrado." });
                
            return Ok(book);
        }

        // POST: api/Book (Similar a Create de PetsController)
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
                // Retorna 201 CreatedAtAction
                return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
            }
            catch (DuplicateEntityException ex)
            {
                // Replicamos el manejo de errores de validación de unicidad
                return BadRequest(new { message = ex.Message }); 
            }
            catch (BusinessRuleException ex)
            {
                // Captura si hay alguna otra regla de negocio
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Book/5 (Similar a Update de PetsController)
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
                // Retornamos 204 No Content para operaciones PUT exitosas que no devuelven cuerpo
                return NoContent(); 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (DuplicateEntityException ex)
            {
                 // Si el ISBN duplicado ocurre durante la actualización
                return BadRequest(new { message = ex.Message });
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Book/5 (Similar a Delete de PetsController)
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var success = await _bookService.DeleteBookAsync(id);
                if (!success)
                {
                    // Si el servicio no pudo eliminar (ej. no encontrado)
                    return NotFound(new { message = $"Libro con ID {id} no encontrado." });
                }
                // Retorna 204 No Content
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BusinessRuleException ex)
            {
                // Captura si el libro tiene préstamos activos (Regla de Negocio)
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}