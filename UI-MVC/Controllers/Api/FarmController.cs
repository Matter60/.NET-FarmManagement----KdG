using FarmManagement.BL;
using FarmManagement.UI.Web.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.UI.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FarmController : ControllerBase
{
    private readonly IManager _manager;
    public FarmController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpPut("{id}")]
    public IActionResult Put(int id, UpdateFarmDto dto)
    {
      
        var farmToUpdate = _manager.GetFarmWithMaintainer(id);
        if (farmToUpdate == null) return NotFound();
        
        bool isAdmin = User.IsInRole("Admin");
        bool isOwner = farmToUpdate.Maintainer.UserName == User.Identity?.Name;

        if (!isAdmin && !isOwner)
        {
            return Forbid(); // 403 Forbidden
        }

       farmToUpdate.SizeInHectares = dto.SizeInHectares;
     
        _manager.ChangeFarm(farmToUpdate);

        return NoContent();
    }
}