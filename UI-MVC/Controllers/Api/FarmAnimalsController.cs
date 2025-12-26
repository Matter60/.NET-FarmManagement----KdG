using FarmManagement.BL;
using FarmManagement.UI.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.UI.Web.Controllers.Api;
[ApiController]
[Route("api/[controller]")]
public class FarmAnimalsController : ControllerBase
{ 
    private readonly IManager _manager;

    public FarmAnimalsController(IManager manager)
    {
        _manager = manager;
    }

    [Route("{farmId}/{animalId}")]
    [HttpGet]
    public IActionResult GetFarmAnimal(int farmId, int animalId)
    {
        var farmAnimal = _manager.GetFarmAnimal(farmId, animalId);
        if (farmAnimal == null)
            return NotFound();
        
        var dto = new FarmAnimalDto
        {
            FarmId = farmId,
            AnimalId = animalId,
            Count = farmAnimal.Count
        };

        return Ok(dto);
            
    }
    
    
    [HttpPost]
    public IActionResult Post(NewFarmAnimalDto newFarmAnimal)
    {
        var farmAnimal = _manager.AddFarmAnimal(newFarmAnimal.FarmId, newFarmAnimal.AnimalId, newFarmAnimal.Count);
       
        var dto = new FarmAnimalDto
        {
            FarmId = newFarmAnimal.FarmId,
            AnimalId = newFarmAnimal.AnimalId,
            Count = farmAnimal.Count
        };
        
        return CreatedAtAction(
            "GetFarmAnimal",
            new
            {
                farmId = farmAnimal.Farm.Id,
                animalId = farmAnimal.Animal.Id
            },
            dto
        );
    }
}