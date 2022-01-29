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
        var assisManager = new Role
        {
            Name = "Assistant Manager",
        };
        var accountant = new Role
        {
            Name = "Accountant",
        };
        var employee = new Employee
        {
            Name = "Jane Doe",
        };
        var posList = new List<DepartmentPosition>()
        {
            new DepartmentPosition() {  Role = accountant, Employee = employee },
            new DepartmentPosition() {  Role = assisManager },
        };
        return View(posList);
    }

    public IActionResult HrApprovalList()
    {
        var assisManager = new Role
        {
            Name = "Assistant Manager",
        };
        var pos = new OpenPosition
        {
            Role = assisManager,
        };
        var posList = new List<OpenPosition>()
        {
            pos,
        };
        return View(posList);
    }
    
    public IActionResult LeadershipApproval()
    {
        var exampleRole = new Role
        {
            Name = "Assistant Manager",
        };
        var examplePos = new OpenPosition()
        {
            Role = exampleRole
        };
        return View(examplePos);
    }
    
    public IActionResult CandidateForm()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}