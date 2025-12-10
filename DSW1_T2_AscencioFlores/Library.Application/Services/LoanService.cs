using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
        {
            var loans = await _unitOfWork.LoanRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        public async Task<LoanDto> CreateLoanAsync(CreateLoanDto loanDto)
        {
            // 1. Validar si el libro existe
            var book = await _unitOfWork.BookRepository.GetByIdAsync(loanDto.BookId);
            if (book == null) throw new Exception("El libro no existe.");

            // 2. REGLA DE NEGOCIO: No prestar si Stock es 0 (Fuente: Reglas de Negocio PDF)
            if (book.Stock <= 0)
            {
                throw new Exception("No hay stock disponible para este libro.");
            }

            // 3. Crear el préstamo
            var loan = _mapper.Map<Loan>(loanDto);
            loan.LoanDate = DateTime.Now;
            loan.Status = "Active";
            loan.CreatedAt = DateTime.Now;

            // 4. REGLA DE NEGOCIO: Disminuir Stock en 1 (Fuente: Reglas de Negocio PDF)
            book.Stock -= 1;

            // 5. Guardar ambas cosas (Transacción implícita con UnitOfWork)
            await _unitOfWork.LoanRepository.AddAsync(loan);
            await _unitOfWork.BookRepository.UpdateAsync(book);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<bool> ReturnLoanAsync(int loanId)
        {
            var loan = await _unitOfWork.LoanRepository.GetByIdAsync(loanId);
            if (loan == null) return false;

            if (loan.Status == "Returned") return false; // Ya fue devuelto

            // 1. Actualizar estado del préstamo
            loan.ReturnDate = DateTime.Now;
            loan.Status = "Returned";

            // 2. REGLA DE NEGOCIO: Aumentar Stock en 1 (Fuente: Reglas de Negocio PDF)
            var book = await _unitOfWork.BookRepository.GetByIdAsync(loan.BookId);
            if (book != null)
            {
                book.Stock += 1;
                await _unitOfWork.BookRepository.UpdateAsync(book);
            }

            await _unitOfWork.LoanRepository.UpdateAsync(loan);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}