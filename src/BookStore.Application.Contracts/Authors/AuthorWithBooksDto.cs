using System;
using System.Collections.Generic;
using BookStore.Books; // Certifique-se de que o BookDto está aqui
using Volo.Abp.Application.Dtos;

namespace BookStore.Authors;

public class AuthorWithBooksDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Bio { get; set; }
    public List<BookDto> Books { get; set; }
}