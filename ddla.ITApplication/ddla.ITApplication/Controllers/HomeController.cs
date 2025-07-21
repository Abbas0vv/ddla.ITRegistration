using ddla.ITApplication.Database;
using ddla.ITApplication.Database.Models.DomainModels;
using ddla.ITApplication.Database.Models.ViewModels.Product;
using ddla.ITApplication.Helpers.Extentions;
using ddla.ITApplication.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
namespace ddla.ITApplication.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }


    public async Task<IActionResult> Table()
    {
        List<Product> model = await _productService.GetAllAsync();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _productService.Insert(model);
        return RedirectToAction(nameof(Table));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (id is null || product is null) return NotFound();

        var model = new UpdateProductViewModel()
        {
            Recipient = product.Recipient,
            Name = product.Name,
            Description = product.Description,
            Count = product.Count,
            DepartmentName = product.Department?.Name,
            UnitName = product.Unit?.Name,
            DateofReceipt = product.DateofReceipt,
            DocumentPath = product.FilePath,
            ImagePath = product.ImageUrl
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int? id, UpdateProductViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        await _productService.Update(id, model);
        return RedirectToAction(nameof(Table));
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        await _productService.Remove(id);
        return RedirectToAction(nameof(Table));
    }
}
