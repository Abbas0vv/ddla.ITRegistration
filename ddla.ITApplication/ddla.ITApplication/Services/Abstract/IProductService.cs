using ddla.ITApplication.Database.Models.DomainModels;
using ddla.ITApplication.Database.Models.ViewModels;

namespace ddla.ITApplication.Services.Abstract;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetSomeAsync(int value);
    Task<Product> GetByIdAsync(int? id);
    Task Insert(CreateProductViewModel model);
}
