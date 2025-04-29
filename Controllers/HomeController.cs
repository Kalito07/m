using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movie_Catalog.Data.Models;
using Movie_Catalog.Models;

namespace Movie_Catalog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MovieCatalogContext _context;
    
    public HomeController(ILogger<HomeController> logger, MovieCatalogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
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
