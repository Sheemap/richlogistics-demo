using System.Diagnostics;
using HexDemoSite.Data;
using Microsoft.AspNetCore.Mvc;
using HexDemoSite.Models;
using HexDemoSite.Services;
using Microsoft.EntityFrameworkCore;

namespace HexDemoSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataContext _context;
    private readonly SendGridService _sendGridService;

    public HomeController(ILogger<HomeController> logger, DataContext context, SendGridService sendGridService)
    {
        _logger = logger;
        _context = context;
        _sendGridService = sendGridService;
    }

    public IActionResult Index()
    {
        return RedirectToAction("DepartmentList");
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult AdminView()
    {
        var posList = _context.DepartmentPositions
            .Include(x => x.Role)
            .Include(x => x.Employee)
            .ToList();
        var roles = _context.Roles.ToList();

        var depModel = new DepartmentListModel()
        {
            Positions = posList,
            AvailableRoles = roles,
        };
        
        var reqList = _context.OpenPositions
            .Include(x => x.DepartmentPosition.Role)
            .Where(x => x.HRDateApproved == null)
            .ToList();
        var hrModel = new HRListModel
        {
            ApprovalQueue = reqList,
            Roles = roles,
        };
        
        var openPosList = _context.OpenPositions
            .Include(x => x.DepartmentPosition.Role)
            .Where(x => x.HRDateApproved != null &&
                        x.LeadershipDateApproved != null &&
                        x.DateFilled == null)
            .ToList();
        
        var candidateList = _context.CandidateForms
            .Include(x => x.OpenPosition)
            .Include(x => x.OpenPosition.DepartmentPosition.Role)
            .Where(x => x.OpenPosition.DateFilled == null)
            .ToList();

        var model = new AdminViewModel
        {
            DepartmentModel = depModel,
            HRModel = hrModel,
            OpenPositionsModel = openPosList,
            CandidateListModel = candidateList,
        };
        
        return View(model);
    }

    [HttpPost]
    public IActionResult CreateDepartmentPosition(DepartmentPosition position)
    {
        position.Department = "Operations";
        _context.DepartmentPositions.Add(position);
        
        var openPos = new OpenPosition
        {
            DepartmentPosition = position,
        };
        _context.OpenPositions.Add(openPos);

        position.OpenPosition = openPos;
        
        _context.SaveChanges();
        
        
        return NoContent();
    }

    public IActionResult DepartmentList()
    {
        var posList = _context.DepartmentPositions
            .Include(x => x.Role)
            .Include(x => x.Employee)
            .ToList();
        var roles = _context.Roles.ToList();

        var model = new DepartmentListModel()
        {
            Positions = posList,
            AvailableRoles = roles,
        };
        return View(model);
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
            DepartmentPosition = depPos,
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
            .Include(x => x.DepartmentPosition.Role)
            .Where(x => x.HRDateApproved == null)
            .ToList();
        var roles = _context.Roles.ToList();
        
        var model = new HRListModel
        {
            ApprovalQueue = reqList,
            Roles = roles,
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult CreateRole(Role model)
    {
        model.Id = 0;
        _context.Roles.Add(model);
        _context.SaveChanges();
        
        return NoContent();
    }
    
    [HttpPost]
    public IActionResult EditRole(Role model)
    {
        var role = _context.Roles.Find(model.Id);
        if (role == null)
        {
            return NotFound();
        }

        role.Name = model.Name;
        _context.SaveChanges();
        
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> HRApproveRequest(int id)
    {
        var openPos = _context.OpenPositions.Find(id);
        if (openPos == null)
        {
            return NotFound();
        }

        var code = Guid.NewGuid().ToString();
        await _sendGridService.SendHireRequestApprovalAsync(id, code);
        
        openPos.HRDateApproved = DateTimeOffset.Now;
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
    
    public IActionResult LeadershipApproval(int id, [FromQuery] string code)
    {
        var openPos = _context.OpenPositions
            .Include(x => x.DepartmentPosition.Role)
            .FirstOrDefault(x => x.Id == id);
        if (openPos == null)
        {
            return NotFound();
        }
        
        var model = new HireApprovalRequest()
        {
            OpenPosition = openPos,
            ApprovalCode = code,
        };
        return View(model);
    }
    
    [HttpPost]
    public IActionResult LeadershipApprove(int id, [FromBody] string code)
    {
        var openPos = _context.OpenPositions.Find(id);
        if (openPos == null)
        {
            return NotFound();
        }
        
        openPos.LeadershipDateApproved = DateTimeOffset.Now;
        _context.SaveChanges();
        
        return NoContent();
    }
    
    [HttpPost]
    public IActionResult LeadershipReject(int id, [FromBody] string code)
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
    
    public IActionResult CandidateForm(int id)
    {
        var openPos = _context.OpenPositions
            .Include(x => x.DepartmentPosition.Role)
            .FirstOrDefault(x => x.Id == id);

        var form = new Candidate()
        {
            OpenPositionId = openPos?.Id ?? default,
            OpenPosition = openPos,
        };
        return View(form);
    }

    [HttpPost]
    public IActionResult CandidateForm(Candidate model)
    {
        var openPos = _context.OpenPositions.Find(model.OpenPositionId);
        if (openPos == null)
        {
            return RedirectToAction("ThanksPage", "Home", new { reason = "applying"});
        }

        // Ensure we add a new row
        model.Id = default;

        _context.CandidateForms.Add(model);
        _context.SaveChanges();
        
        return RedirectToAction("ThanksPage", "Home", new { reason = "applying"});
    }
    
    public IActionResult OpenPositionList()
    {
        var posList = _context.OpenPositions
            .Include(x => x.DepartmentPosition.Role)
            .Where(x => x.HRDateApproved != null &&
                                  x.LeadershipDateApproved != null &&
                                  x.DateFilled == null)
            .ToList();
        return View(posList);
    }

    public IActionResult CandidateList()
    {
        var candidateList = _context.CandidateForms
            .Include(x => x.OpenPosition)
            .Include(x => x.OpenPosition.DepartmentPosition.Role)
            .Where(x => x.OpenPosition.DateFilled == null)
            .ToList();
        return View(candidateList);
    }

    public async Task<IActionResult> SelectCandidate(int id)
    {
        var candidate = _context.CandidateForms
            .Include(x => x.OpenPosition)
            .Include(x => x.OpenPosition.DepartmentPosition)
            .Include(x => x.OpenPosition.DepartmentPosition.Role)
            .FirstOrDefault(x => x.Id ==id);
        if (candidate == null)
        {
            return NotFound();
        }
        
        candidate.OpenPosition.DateFilled = DateTimeOffset.Now;

        var employee = new Employee()
        {
            Name = candidate.FirstName + " " + candidate.LastName,
        };
        candidate.OpenPosition.DepartmentPosition.Employee = employee;

        _context.SaveChanges();

        await _sendGridService.SendHiredEmailAsync(candidate.Id, candidate.Email,
            candidate.OpenPosition.DepartmentPosition.Role.Name);

        var rejected = _context.CandidateForms
            .Where(x => x.OpenPositionId == candidate.OpenPositionId && 
                        x.Id != candidate.Id)
            .Select(x => x.Email)
            .ToArray();
        
        await _sendGridService.SendRejectedEmailsAsync(rejected);
        
        return NoContent();
    }

    public IActionResult CandidateConfirm(int id)
    {
        var candidate = _context.CandidateForms.Find(id);
        if (candidate == null)
        {
            return NotFound();
        }
        
        return View(candidate);
    }

    [HttpPost]
    public IActionResult CandidateConfirm(Candidate model)
    {
        _context.CandidateForms.Attach(model);
        _context.Entry(model).State = EntityState.Modified;
        _context.SaveChanges();
        
        // TODO: Add ticket to IT system
        return RedirectToAction("ThanksPage", "Home", new { reason = "confirm your information"});
    }
    
    public IActionResult ThanksPage([FromQuery] string reason)
    {
        ViewBag.Reason = reason;
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}