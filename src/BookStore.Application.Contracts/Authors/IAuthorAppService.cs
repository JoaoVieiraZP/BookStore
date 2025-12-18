using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Authors;

public interface IAuthorAppService :
    ICrudAppService< // O ABP já nos dá o CRUD pronto
        AuthorDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateAuthorDto,
        UpdateAuthorDto>
{
    Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();
    Task<AuthorWithBooksDto> GetWithBooksAsync(Guid id);
}