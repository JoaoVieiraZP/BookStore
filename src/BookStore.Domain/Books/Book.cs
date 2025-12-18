using System;
using System.ComponentModel.DataAnnotations.Schema;
using BookStore.Authors;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.Books;

public class Book : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
    public BookType Type { get; set; }
    public DateTime PublishDate { get; set; }
    public float Price { get; set; }

    public Guid AuthorId { get; set; }

    [ForeignKey("AuthorId")] //* Isso força o uso da propriedade acima e evita o "AuthorId1"
    public virtual Author Author { get; set; } = default!;
}