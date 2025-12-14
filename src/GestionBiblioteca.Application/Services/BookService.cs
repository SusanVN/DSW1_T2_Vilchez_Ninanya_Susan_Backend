using AutoMapper;
using GestionBiblioteca.Application.DTOs;
using GestionBiblioteca.Application.Interfaces;
using GestionBiblioteca.Domain.Entities;
using GestionBiblioteca.Domain.Exceptions;
using GestionBiblioteca.Domain.Ports.Out;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBiblioteca.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null) throw new NotFoundException("Libro", id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto dto)
        {
           
            var existing = await _unitOfWork.Books.GetByISBNAsync(dto.ISBN);
            if (existing != null) throw new DuplicateEntityException("Libro", "ISBN", dto.ISBN);

            var book = _mapper.Map<Book>(dto);
            
            book.CreatedAt = DateTime.Now; 

            var created = await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BookDto>(created);
        }

        public async Task<bool> UpdateBookAsync(int id, CreateBookDto dto)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null) throw new NotFoundException("Libro", id);

            
            var existingWithIsbn = await _unitOfWork.Books.GetByISBNAsync(dto.ISBN);
            if (existingWithIsbn != null && existingWithIsbn.Id != id)
                throw new DuplicateEntityException("Libro", "ISBN", dto.ISBN);

            _mapper.Map(dto, book);
            await _unitOfWork.Books.UpdateAsync(book);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var success = await _unitOfWork.Books.DeleteAsync(id);
            if (success) await _unitOfWork.SaveChangesAsync();
            return success;
        }

        public async Task<BookDto?> GetBookByISBNAsync(string isbn)
        {
            
            var book = await _unitOfWork.Books.GetByISBNAsync(isbn);
            
            
            if (book == null) return null;
            
            
            return _mapper.Map<BookDto>(book);
        }
    }
}