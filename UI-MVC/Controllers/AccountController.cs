using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.UI.Web.Controllers;

public class AccountController : Controller
{
    public async Task<IActionResult > LogIn(string username, string password)
    {
        var claims = new List<Claim>() {
            new Claim(ClaimTypes.Name, username)
        };
        var userIdentity = new ClaimsIdentity (claims, "Cookies");
        var userPrincipal = new ClaimsPrincipal (userIdentity);
        await this.HttpContext.SignInAsync (userPrincipal);
       
        return RedirectToAction ("Index", "Home");
    }
    public async Task<IActionResult > LogOut()
    {
        await this.HttpContext.SignOutAsync ();
        return RedirectToAction ("Index", "Home");
    }
}