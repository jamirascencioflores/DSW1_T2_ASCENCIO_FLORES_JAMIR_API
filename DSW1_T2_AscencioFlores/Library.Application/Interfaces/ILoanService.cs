using Library.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDto>> GetAllLoansAsync();
        Task<LoanDto> CreateLoanAsync(CreateLoanDto loanDto);
        Task<bool> ReturnLoanAsync(int loanId);
    }
}