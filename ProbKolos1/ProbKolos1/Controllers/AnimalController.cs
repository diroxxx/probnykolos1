using System.Transactions;
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

    [HttpPost]
    [Route("api/animals")]
    public async Task<IActionResult> AddAnimal(AddAnimal animal)
    {
        if (! await _animalRepository.DoesOwnerExist(animal.OwnerId))
        {
            return NotFound("Given Owner doesn't exist");
        }

        for (int i = 0; i < animal.Procedures.Count; i++)
        {
            if (! await _animalRepository.DoesProcedureExist(animal.Procedures[i].ProcedureId))
            {
                return NotFound("Given procedure doesn't exist");
            }
        }

        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
           await _animalRepository.AddAnimal(animal); 
           
           scope.Complete();
        }
        
        

        

        return Created(Request.Path.Value ?? "api/animals", animal);
        
    }

    
}