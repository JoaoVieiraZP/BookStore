using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Authors;

public class UpdateAuthorDto
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    public string Bio { get; set; }
}