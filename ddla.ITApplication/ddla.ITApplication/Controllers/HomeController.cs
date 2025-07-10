using ddla.ITApplication.Database;
using ddla.ITApplication.Database.Models.DomainModels;
using ddla.ITApplication.Database.Models.ViewModels;
using ddla.ITApplication.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
namespace ddla.ITApplication.Controllers;

public class HomeController : Controller
{
    private readonly ddlaITAppDBContext _context;
    private readonly IProductService _productService;

    public HomeController(ddlaITAppDBContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        List<Product> model = await _productService.GetAllAsync();

        return View(model);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        await _productService.Insert(model);
        return RedirectToAction(nameof(Index));
    }
}
