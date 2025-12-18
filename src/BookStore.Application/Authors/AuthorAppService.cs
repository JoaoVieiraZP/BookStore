using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Books;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Authors;

public class AuthorAppService :
    CrudAppService<
        Author, 
        AuthorDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateAuthorDto,
        UpdateAuthorDto>,
    IAuthorAppService
{
    // 1. Declare o campo privado para o repositório de livros
    private readonly AuthorManager _authorManager;
    private readonly IRepository<Book, Guid> _bookRepository;

    // 2. Adicione o repositório no construtor
    public AuthorAppService(
        IAuthorRepository repository,
        AuthorManager authorManager,
        IRepository<Book, Guid> bookRepository) // Injeção aqui
        : base(repository)
    {
        _authorManager = authorManager;
        _bookRepository = bookRepository;
    }
    
    public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()

    {
        var authors = await Repository.GetListAsync();
        return new ListResultDto<AuthorLookupDto>(
            ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
        );
    }

    // 3. Implemente o novo método de detalhes
    public async Task<AuthorWithBooksDto> GetWithBooksAsync(Guid id)
    {
        var author = await Repository.GetAsync(id);
        var books = await _bookRepository.GetListAsync(x => x.AuthorId == id);
        var dto = ObjectMapper.Map<Author, AuthorWithBooksDto>(author);
        dto.Books = ObjectMapper.Map<List<Book>, List<BookDto>>(books);
        return dto;
    }
    
    public override async Task<AuthorDto> CreateAsync(CreateAuthorDto input)
    {
        // Aqui você extrai os dados do DTO e passa para o Manager
        var author = await _authorManager.CreateAsync(
            input.Name,
            input.BirthDate,
            input.Bio
        );

        await Repository.InsertAsync(author);

        return ObjectMapper.Map<Author, AuthorDto>(author);
    }
}