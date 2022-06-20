using System.ComponentModel.DataAnnotations;

namespace TestAPI.DTOs;

public class CreatePersonneDTO
{
    [Required]
    public string Nom { get; set; }

    [Required]
    public string Prenom { get; set; }

    [Required]
    public DateTime DateNaissance { get; set; }
}