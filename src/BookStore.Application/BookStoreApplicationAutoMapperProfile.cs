using AutoMapper;
using BookStore.Authors;
using BookStore.Books;

namespace BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<BookDto, CreateUpdateBookDto>(); 
        CreateMap<Author, AuthorDto>();
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<UpdateAuthorDto, Author>();
        CreateMap<AuthorDto, UpdateAuthorDto>();
        CreateMap<Author, AuthorLookupDto>();
        CreateMap<Author, AuthorWithBooksDto>();
    }
}