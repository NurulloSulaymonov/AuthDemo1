using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Automapper;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Quote,GetQuoteDto>()
            .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(src=>src.Category.Name));
        
        CreateMap<AddQuoteDto, Quote>()
            .ForMember(dest=>dest.CreatedAt,opt=>opt.MapFrom(src=>DateTime.UtcNow))
            .ForMember(dest=>dest.Author,opt=>opt.MapFrom(src=>src.AuthorName))
            .ForMember(dest=>dest.QuoteText,opt=>opt.MapFrom(src=>src.Title))
            ;
    }
}