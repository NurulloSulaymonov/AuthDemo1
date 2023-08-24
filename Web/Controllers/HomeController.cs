using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Views.Home;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    [Authorize]
    public IActionResult About()
    {
        return View();
    }
    
    [Authorize(Roles = "Admin,Teacher")]
    public IActionResult Products()
    {
        var products = new List<Product>()
        {
            new Product("ahmad", "Muhammad", "test"),
            new Product("ahmad", "Muhammad", "test"),
            new Product("ahmad", "Muhammad", "test"),
            new Product("ahmad", "Muhammad", "test"),
            new Product("ahmad", "Muhammad", "test"),
        };
        return View(products);
    }

    public IActionResult QueryString(string text,string text2)
    {
        return Json(new {text1 = text, text2});
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
