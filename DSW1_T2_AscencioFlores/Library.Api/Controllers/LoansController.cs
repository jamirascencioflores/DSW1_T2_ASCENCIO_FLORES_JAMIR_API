using Library.Application.DTOs;
using Library.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _loanService.GetAllLoansAsync();
            return Ok(loans);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLoanDto loanDto)
        {
            try
            {
                var createdLoan = await _loanService.CreateLoanAsync(loanDto);
                return Ok(createdLoan);
            }
            catch (Exception ex)
            {
                // Aquí capturamos la Regla de Negocio (Stock 0)
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("return/{id}")]
        public async Task<IActionResult> ReturnLoan(int id)
        {
            var result = await _loanService.ReturnLoanAsync(id);
            if (!result) return BadRequest(new { message = "No se pudo devolver el libro. Verifique el ID o si ya fue devuelto." });

            return Ok(new { message = "Libro devuelto correctamente." });
        }
    }
}