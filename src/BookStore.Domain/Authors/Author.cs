using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.Authors;

public class Author : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Bio { get; set; } = string.Empty; // Inicializa para remover o warning

    private Author() { } // Para o EF Core

    internal Author(Guid id, string name, DateTime birthDate, string? bio = null)
        : base(id)
    {
        SetName(name);
        BirthDate = birthDate;
        Bio = bio ?? string.Empty;
    }

    internal void ChangeName(string name) // O Manager chamará este método
    {
        SetName(name);
    }

    private void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLength: 128);
    }
}