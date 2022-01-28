using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HexDemoSite.Models;

namespace HexDemoSite.Controllers;

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

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult DepartmentList()
    {
        return View(new List<DepartmentPosition>());
    }
    
    // public IActionResult RequestPosition()
    // {
    //     return View(new List<DepartmentPosition>());
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}