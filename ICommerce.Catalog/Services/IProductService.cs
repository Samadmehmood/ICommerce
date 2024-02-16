using ICommerce.Catalog.Models;
using ICommerce.Contracts;

namespace ICommerce.Catalog.Services;

public interface IProductService{
    Task<ServiceResponse<Product>> CreateProduct(Product product);
    Task<ServiceResponse<Product>> UpsertProduct(Product product);
    Task<ServiceResponse<string>> DeleteProduct(Guid id);
    Task<ServiceResponse<Product>> GetProductById(Guid id);
    Task<ServiceResponse<IEnumerable<Product>>> GetAllProducts();

}