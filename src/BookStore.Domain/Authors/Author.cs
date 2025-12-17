using System;
using System.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.Authors;

public class Author : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string Bio { get; set; }
    
    private Author() { }
    
    public Author(Guid id, string name, DateTime birthDate, string bio) : base(id)
    {
        Name = name;
        BirthDate = birthDate;
        Bio = bio;
    }
}