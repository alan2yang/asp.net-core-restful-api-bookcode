using AutoMapper;
using Library.API.Entities;
using Library.API.Extentions;
using Library.API.Models;

namespace Library.API.Helpers
{
    public class LibraryMappingProfile : Profile
    {
        public LibraryMappingProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Age, config =>
                    config.MapFrom(src => src.BirthDate.GetCurrentAge()));
            CreateMap<AuthorForCreationDto, Author>();
            CreateMap<Book, BookDto>();
            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>();
        }
    }
}