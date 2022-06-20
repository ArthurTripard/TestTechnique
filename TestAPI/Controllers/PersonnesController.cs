using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.DTOs;
using TestAPI.Models;

namespace TestAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonnesController : ControllerBase
{
    private readonly AppDbContext _context;

    public PersonnesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonneDTO>>> GetAllPersonnes()
    {
        var personnes = await _context.Personnes
            .OrderBy(p => p.Nom)
            .ThenBy(p => p.Prenom)
            .ToListAsync();

        return personnes.ConvertAll(p => new PersonneDTO
        {
            Id = p.Id,
            Nom = p.Nom,
            Prenom = p.Prenom,
            DateNaissance = p.DateNaissance,
            Age = DateTime.Today.Year - p.DateNaissance.Year
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreatePersonne(CreatePersonneDTO personne)
    {
        var age = DateTime.Today.Year - personne.DateNaissance.Year;
        if (age >= 150)
        {
            ModelState.AddModelError("DateNaissance", "Seule les personnes de moins de 150 ans peuvent être enregistrées.");
            return BadRequest(ModelState);
        }

        var newPersonne = new Personne
        {
            Nom = personne.Nom,
            Prenom = personne.Prenom,
            DateNaissance = personne.DateNaissance,
        };

        _context.Personnes.Add(newPersonne);
        await _context.SaveChangesAsync();

        return Ok();
    }
}