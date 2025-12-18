using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Authors;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.BlobStoring;

namespace BookStore.Books;

public class BookAppService :
    CrudAppService<
        Book, 
        BookDto, 
        Guid, 
        GetBookListDto, 
        CreateUpdateBookDto>,
    IBookAppService
{
    private readonly IBlobContainer _blobContainer;
    private readonly IRepository<Author, Guid> _authorRepository;

    public BookAppService(
        IRepository<Book, Guid> repository,
        IRepository<Author, Guid> authorRepository,
        IBlobContainer blobContainer) 
        : base(repository)
    {
        _authorRepository = authorRepository;
        _blobContainer = blobContainer;
    }

    protected override async Task<IQueryable<Book>> CreateFilteredQueryAsync(GetBookListDto input)
    {
        var query = await base.CreateFilteredQueryAsync(input);
        
        return query
            .Include(x => x.Author)
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => 
                x.Name.Contains(input.Filter!) || 
                x.Author.Name.Contains(input.Filter!));
    }

    public override async Task<BookDto> GetAsync(Guid id)
    {
        var queryable = await Repository.GetQueryableAsync();
        var query = queryable.Include(x => x.Author).Where(x => x.Id == id);
        var book = await AsyncExecuter.FirstOrDefaultAsync(query);
        
        var dto = ObjectMapper.Map<Book, BookDto>(book);

        if (dto?.Author != null)
        {
            try
            {
                // Puxa os bytes do Azurite. Certifique-se que o Seeder salvou como .png
                dto.Author.PhotoBytes = await _blobContainer.GetAllBytesOrNullAsync($"{dto.AuthorId}.png");
            }
            catch { }
        }

        return dto!;
    }

    public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
    {
        var authors = await _authorRepository.GetListAsync();

        return new ListResultDto<AuthorLookupDto>(
            ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
        );
    }
}