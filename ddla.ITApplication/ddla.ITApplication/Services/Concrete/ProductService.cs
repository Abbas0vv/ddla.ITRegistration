using ddla.ITApplication.Database;
using ddla.ITApplication.Database.Models.DomainModels;
using ddla.ITApplication.Database.Models.ViewModels;
using ddla.ITApplication.Helpers;
using ddla.ITApplication.Helpers.Extentions;
using ddla.ITApplication.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ddla.ITApplication.Services.Concrete;

public class ProductService : IProductService
{
    private readonly ddlaITAppDBContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private const string FOLDER_NAME = "Uploads/Products";
    public ProductService(ddlaITAppDBContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.OrderBy(p => p.Id).ToListAsync();
    }
    public async Task<List<Product>> GetSomeAsync(int value)
    {
        var numberOfProducts = await _context.Products.CountAsync();
        if (numberOfProducts <= value) return await GetAllAsync();
        return await _context.Products.OrderBy(s => s.Id).Take(value).ToListAsync();
    }
    public async Task<Product> GetByIdAsync(int? id)
    {
        return await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task Insert(CreateProductViewModel model)
    {
        var product = new Product()
        {
            Name = model.Name,
            Description = model.Description,
            DepartmentId = await _context.Departments
                .Where(d => d.Name == model.DepartmentName)
                .Select(d => d.Id)
                .FirstOrDefaultAsync(),
            UnitId = await _context.Units
                .Where(u => u.Name == model.UnitName)
                .Select(u => u.Id)
                .FirstOrDefaultAsync(),
            Unit = await _context.Units
                .Where(u => u.Name == model.UnitName)
                .FirstOrDefaultAsync(),
            Department = await _context.Departments
                .Where(d => d.Name == model.DepartmentName)
                .FirstOrDefaultAsync(),
            Count = model.Count,
            DateofIssue = DateTime.Now,
            DateofReceipt = model.DateofReceipt,
            InventarId = IDGenerator.GenerateId(),
            ImageUrl = model.ImageFile.CreateFile(_webHostEnvironment.WebRootPath, FOLDER_NAME)
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }
}
