using FarmManagement.BL;
using FarmManagement.UI.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.UI.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IManager _manager;

    public AnimalsController(IManager manager)
    {
        _manager = manager;
    }


    [Route("{animalId}/farms")]
    [HttpGet]
    public IActionResult GetFarmsOfAnimal(int animalId)
    {
        var farms = _manager.GetFarmsOfAnimal(animalId);
        if (!(farms?.Any() ?? false))
            return NoContent();
        
        return Ok(farms);
    }

    [Route("{animalId}/farms-candidates")]
    [HttpGet]

    public IActionResult GetAvailableFarmsOfAnimal(int animalId)
    {
        var farms = _manager.GetAvailableFarmsOfAnimal(animalId);
        if (!(farms?.Any() ?? false))
            return NoContent();
        
        return Ok(farms);
    }
    
}