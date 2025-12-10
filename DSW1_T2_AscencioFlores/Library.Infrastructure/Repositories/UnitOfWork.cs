using Library.Domain.Ports.Out;
using Library.Infrastructure.Contexts;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IBookRepository bookRepository, ILoanRepository loanRepository)
        {
            _context = context;
            BookRepository = bookRepository;
            LoanRepository = loanRepository;
        }

        public IBookRepository BookRepository { get; }
        public ILoanRepository LoanRepository { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}