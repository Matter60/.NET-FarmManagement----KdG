using FarmManagement.BL;
using FarmManagement.BL.Domain;
using FarmManagement.UI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.UI.Web.Controllers;

[AutoValidateAntiforgeryToken]
[Authorize]
public class FarmController : Controller
{
    private readonly IManager _manager;
    private readonly UserManager<IdentityUser> _userManager;

    public FarmController(IManager manager, UserManager<IdentityUser> userManager)
    {
        _manager = manager;
        _userManager = userManager;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        var farms = _manager.GetAllFarms();
        return View(farms);
    }
    
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Details(int id)
    {
        var farm = _manager.GetFarmWithAnimalsAndMaintainer(id);
        return View(farm);
    }
    
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(NewFarmViewModel newFarm)
    {
        if (!ModelState.IsValid)
        {
            return View(newFarm);
        }

        var currentLoggedInUser = _userManager.GetUserName(User);
        
        var addedFarm = _manager.AddFarm(newFarm.Name,newFarm.Location, newFarm.EstablishedYear,newFarm.SizeInHectares, currentLoggedInUser);

        return RedirectToAction("Details", new {id = addedFarm.Id});
    }
    
}