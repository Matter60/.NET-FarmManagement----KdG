using FarmManagement.BL;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.UI.Web.Controllers;

public class HarvestController : Controller
{
    private readonly IManager _manager;

    public HarvestController(IManager manager)
    {
        _manager = manager;
    }
    
    public IActionResult Index()
    {
      return View();
    }
}