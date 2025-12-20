using FarmManagement.BL;
using FarmManagement.BL.Domain;
using FarmManagement.UI.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.UI.Web.Controllers;

public class FarmController : Controller
{
    private readonly IManager _manager;

    public FarmController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var farms = _manager.GetAllFarms();
        return View(farms);
    }
    
    
    [HttpGet]
    public IActionResult Details(int id)
    {
        var farm = _manager.GetFarmWithAnimals(id);
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
        
        var addedFarm = _manager.AddFarm(newFarm.Name,newFarm.Location, newFarm.EstablishedYear,newFarm.SizeInHectares);

        return RedirectToAction("Details", new {id = addedFarm.Id});
    }
    
    
}