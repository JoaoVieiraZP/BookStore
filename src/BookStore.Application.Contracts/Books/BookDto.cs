using System;
using BookStore.Authors;
using Volo.Abp.Application.Dtos;

namespace BookStore.Books;

public class BookDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; }
    public BookType Type { get; set; }
    public DateTime PublishDate { get; set; }
    public float Price { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; } //* Para exibir na Grid
    public AuthorDto Author { get; set; } = new();
}