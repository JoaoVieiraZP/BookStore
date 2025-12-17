using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Books;

public interface IBookAppService :
    ICrudAppService< // Define os métodos CRUD (Get, GetList, Create, Update, Delete) automaticamente
        BookDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdateBookDto>
{
}