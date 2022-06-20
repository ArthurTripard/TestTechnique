namespace TestAPI.DTOs;

public class PersonneDTO
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public DateTime DateNaissance { get; set; }
    public int Age { get; set; }
}