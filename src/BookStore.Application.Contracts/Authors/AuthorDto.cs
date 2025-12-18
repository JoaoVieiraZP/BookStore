using System;
using Volo.Abp.Application.Dtos;

namespace BookStore.Authors;

public class AuthorDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Bio { get; set; }
    
    public byte[]? PhotoBytes { get; set; }
    //* Propriedade para a URL da imagem no Blob Storage
    public string PhotoUrl => $"http://127.0.0.1:10000/devstoreaccount1/authors-images/{Id}.png";

    //* Lógica para calcular a idade baseada na data atual
    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}