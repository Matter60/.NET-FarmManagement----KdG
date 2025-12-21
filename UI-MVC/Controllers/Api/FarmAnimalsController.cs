using FarmManagement.BL;
using FarmManagement.UI.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.UI.Web.Controllers.Api;
[ApiController]
[Route("api/[controller]")]
public class FarmAnimalsController : ControllerBase
{ private readonly IManager _manager;

    public FarmAnimalsController(IManager manager)
    {
        _manager = manager;
    }
    
    
    [HttpPost]
    public IActionResult Post(NewFarmAnimalDto newFarmAnimal)
    {
        var farmAnimal = _manager.AddFarmAnimal(newFarmAnimal.FarmId, newFarmAnimal.AnimalId, newFarmAnimal.Count);
        return Ok();
    }
}