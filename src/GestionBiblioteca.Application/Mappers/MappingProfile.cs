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
          
            // Mapeo de BOOKS            
            
            // Entidad a DTO (Lectura/Salida)
            CreateMap<Book, BookDto>();
            
            // DTO a Entidad (Creación/Actualización)
            CreateMap<CreateBookDto, Book>()
                
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()); 


            
            // Mapeo de LOANS 
            

            // Entidad a DTO (Lectura/Salida)
            CreateMap<Loan, LoanDto>()
                
                .ForMember(dest => dest.BookTitle, opt => opt.Ignore()); 

            // DTO a Entidad (Creación)
            CreateMap<CreateLoanDto, Loan>()
                
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.LoanDate, opt => opt.Ignore())
                .ForMember(dest => dest.ReturnDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}


