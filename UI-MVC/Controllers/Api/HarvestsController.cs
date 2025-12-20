using FarmManagement.BL;
using FarmManagement.BL.Domain;
using FarmManagement.UI.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.UI.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class HarvestsController : ControllerBase
{
    private readonly IManager _manager;

    public HarvestsController(IManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IActionResult GetAllHarvests()
    {
        var harvests = _manager.GetAllHarvests();
        if (!(harvests?.Any() ?? false))
            return NoContent();
        
        return Ok(harvests);
    }

    [HttpPost]

    public IActionResult Post(NewHarvestDto newHarvest)
    {
        
        var createdHarvest = _manager.AddHarvest(
            newHarvest.CropType,
            newHarvest.HarvestDate, 
            newHarvest.Quantity, 
            null
        );

      
        return CreatedAtAction(nameof(GetAllHarvests), new { id = createdHarvest.Id }, createdHarvest);
    }
}