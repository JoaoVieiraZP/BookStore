using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace BookStore.Books;

public interface IBookManager
{
    Task CheckDuplicateNameAsync(string name, Guid? expectedId = null);
}