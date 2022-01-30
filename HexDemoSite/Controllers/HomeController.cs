using System.Diagnostics;
using HexDemoSite.Data;
using Microsoft.AspNetCore.Mvc;
using HexDemoSite.Models;
using Microsoft.EntityFrameworkCore;

namespace HexDemoSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataContext _context;

    public HomeController(ILogger<HomeController> logger, DataContext context)
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
    
    public IActionResult DepartmentList()
    {
        var posList = _context.DepartmentPositions
            .Include(x => x.Role)
            .Include(x => x.Employee)
            .ToList();
        return View(posList);
    }

    [HttpPost]
    public IActionResult RequestPosition(int id)
    {
        var depPos = _context.DepartmentPositions.Find(id);
        if (depPos == null)
        {
            return NotFound();
        }

        var openPos = new OpenPosition
        {
            RoleId = depPos.RoleId,
        };
        _context.OpenPositions.Add(openPos);

        depPos.OpenPosition = openPos;
        
        _context.SaveChanges();
        
        return NoContent();
    }
    
    [HttpPost]
    public IActionResult CancelPositionRequest(int id)
    {
        var openPos = _context.OpenPositions.Find(id);
        if (openPos == null)
        {
            return NotFound();
        }

        _context.OpenPositions.Remove(openPos);
        _context.SaveChanges();
        
        return NoContent();
    }

    public IActionResult HrApprovalList()
    {
        var reqList = _context.OpenPositions
            .Include(x => x.Role)
            .Where(x => x.DateApproved == null)
            .ToList();
        return View(reqList);
    }
    
    [HttpPost]
    public IActionResult HRApproveRequest(int id)
    {
        var openPos = _context.OpenPositions.Find(id);
        if (openPos == null)
        {
            return NotFound();
        }
        
        openPos.DateApproved = DateTimeOffset.Now;
        _context.SaveChanges();
        
        return NoContent();
    }
    
    [HttpPost]
    public IActionResult HRDeclineRequest(int id)
    {
        var openPos = _context.OpenPositions.Find(id);
        if (openPos == null)
        {
            return NotFound();
        }

        _context.OpenPositions.Remove(openPos);
        _context.SaveChanges();
        
        return NoContent();
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