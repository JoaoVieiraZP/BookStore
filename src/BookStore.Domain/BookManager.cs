using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace BookStore.Books;

public class BookManager : DomainService, IBookManager
{
    private readonly IRepository<Book, Guid> _bookRepository;

    public BookManager(IRepository<Book, Guid> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    //* Este único método agora faz todo o trabalho
    public async Task CheckDuplicateNameAsync(string name, Guid? expectedId = null)
    {
        var existingBook = await _bookRepository.FirstOrDefaultAsync(b => b.Name == name);
    
        //* Se encontrou um livro COM O MESMO NOME, mas o ID é DIFERENTE do que estamos editando
        if (existingBook != null && existingBook.Id != expectedId)
        {
            throw new UserFriendlyException($"Já existe um livro cadastrado com o nome: {name}");
        }
    }
}