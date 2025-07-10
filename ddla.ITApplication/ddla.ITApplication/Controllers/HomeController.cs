using ddla.ITApplication.Database;
using ddla.ITApplication.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ddla.ITApplication.Controllers;

public class HomeController : Controller
{
    private readonly ddlaITAppDBContext _context;

    public HomeController(ddlaITAppDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = _context.Products
            .Include(p => p.Department)
            .Include(p => p.Unit)
            .Select(p => new {
                Product = p,
                DepartmentName = p.Department.Name,
                ShobeName = p.Unit.Name
            })
            .ToList();

        return View(model);
    }
}
