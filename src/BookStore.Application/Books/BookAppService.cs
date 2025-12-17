using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Books;

public class BookAppService :
    CrudAppService<Book, BookDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBookDto>, 
    IBookAppService 
{
    private readonly IBookManager _bookManager;

    public BookAppService(
        IRepository<Book, Guid> repository,
        IBookManager bookManager) //* Injetando o novo serviço
        : base(repository)
    {
        _bookManager = bookManager;
    }

    public override async Task<BookDto> CreateAsync(CreateUpdateBookDto input)
    {
        //* No Create, não temos ID ainda, então enviamos apenas o nome
        await _bookManager.CheckDuplicateNameAsync(input.Name);
        return await base.CreateAsync(input);
    }

    public override async Task<BookDto> UpdateAsync(Guid id, CreateUpdateBookDto input)
    {
        //* No Update, enviamos o nome E o ID atual do livro
        await _bookManager.CheckDuplicateNameAsync(input.Name, id);
        return await base.UpdateAsync(id, input);
    }
}