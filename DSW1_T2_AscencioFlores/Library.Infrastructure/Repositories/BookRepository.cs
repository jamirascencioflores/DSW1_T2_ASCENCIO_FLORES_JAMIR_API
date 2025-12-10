using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using Library.Infrastructure.Contexts;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}