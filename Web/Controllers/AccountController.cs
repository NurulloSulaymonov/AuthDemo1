using System.Security.Claims;
using Domain.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (model.Username == "test" && model.Password == "test1")
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,model.Username),
                new Claim(ClaimTypes.Role,"Admin"),
                
            };

            var identity = new ClaimsIdentity(claims, "UserIdentity");
            await  HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(1),
                IsPersistent = true,
            });
        }

        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
   
    

}