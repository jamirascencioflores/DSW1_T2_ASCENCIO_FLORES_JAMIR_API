using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using Library.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class LoanRepository : Repository<Loan>, ILoanRepository
    {
        public LoanRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Loan>> GetAllWithDetailsAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .ToListAsync();
        }
    }
}