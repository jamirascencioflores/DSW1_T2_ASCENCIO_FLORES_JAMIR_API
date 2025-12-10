using Library.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task<BookDto> CreateBookAsync(CreateBookDto bookDto);
        Task<bool> UpdateBookAsync(int id, CreateBookDto bookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}