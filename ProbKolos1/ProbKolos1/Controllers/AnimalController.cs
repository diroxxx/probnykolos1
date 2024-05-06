using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProbKolos1.DTOs;
using ProbKolos1.Repositories;

namespace ProbKolos1.Controllers;

[ApiController]
// [Route("api/[controller]")]
public class AnimalController: ControllerBase
{

    private readonly IAnimalRepository _animalRepository;

    public AnimalController(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    
    [HttpGet]
    [Route("api/animals/")]
    public async Task<IActionResult> AnimalInfo(int id)
    {
        if (! await _animalRepository.DoesAnimalExist(id))
        {
            return NotFound("Animal doesn't exist with given id");
        }

        var animalInfo = await _animalRepository.GetAnimal(id);

        return Ok(animalInfo);
        

    }

    
    
    
}