using System.Security.Claims;
using Domain.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Login(string? ReturnUrl)
    {
        return View(new LoginDto()
        {
            ReturnUrl = ReturnUrl
        });
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
                ExpiresUtc = DateTime.UtcNow.AddHours(1),
                IsPersistent = true,
            });
            
            
            if (model.ReturnUrl != null)
                return Redirect(model.ReturnUrl);
            else return RedirectToAction("index", "Home");
        }
        else
        {
            return View(model);
        }

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