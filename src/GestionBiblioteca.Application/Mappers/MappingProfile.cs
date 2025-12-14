using AutoMapper;
using GestionBiblioteca.Application.DTOs;
using GestionBiblioteca.Domain.Entities;
using System;

namespace GestionBiblioteca.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
                             
                        
            CreateMap<Book, BookDto>();
            
            
            CreateMap<CreateBookDto, Book>()
                
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()); 


            
            
            CreateMap<Loan, LoanDto>()
                
                .ForMember(dest => dest.BookTitle, opt => opt.Ignore()); 

            
            CreateMap<CreateLoanDto, Loan>()
                
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.LoanDate, opt => opt.Ignore())
                .ForMember(dest => dest.ReturnDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}


