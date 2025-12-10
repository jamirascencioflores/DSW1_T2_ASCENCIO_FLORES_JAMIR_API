using AutoMapper;
using Library.Application.DTOs;
using Library.Domain.Entities;

namespace Library.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeos de Libros
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<CreateBookDto, Book>();

            // Mapeos de Préstamos
            CreateMap<Loan, LoanDto>()
                // Corrección: Verificamos si src.Book es nulo antes de acceder a Title
                .ForMember(dest => dest.BookTitle,
                           opt => opt.MapFrom(src => src.Book != null ? src.Book.Title : "Desconocido"));

            CreateMap<CreateLoanDto, Loan>();
        }
    }
}