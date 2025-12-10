using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Application.Services
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
            var books = await _unitOfWork.BookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            book.CreatedAt = System.DateTime.Now;

            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> UpdateBookAsync(int id, CreateBookDto bookDto)
        {
            var existingBook = await _unitOfWork.BookRepository.GetByIdAsync(id);
            if (existingBook == null) return false;

            // Actualizamos campos
            existingBook.Title = bookDto.Title;
            existingBook.Author = bookDto.Author;
            existingBook.ISBN = bookDto.ISBN;
            existingBook.Stock = bookDto.Stock;

            await _unitOfWork.BookRepository.UpdateAsync(existingBook);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var deleted = await _unitOfWork.BookRepository.DeleteAsync(id);
            if (deleted) await _unitOfWork.SaveChangesAsync();
            return deleted;
        }
    }
}