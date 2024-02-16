using ICommerce.Catalog.Models;
using ICommerce.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace ICommerce.Catalog.Services;

public class ProductService : IProductService
{
    private readonly Dictionary<Guid, Product> _productCache;

    public ProductService()
    {
        _productCache = new Dictionary<Guid, Product>();
    }

    public async Task<ServiceResponse<Product>> CreateProduct(Product product)
    {
        try
        {
            _productCache.Add(product.Id, product);

            var data = product;

            return new ServiceResponse<Product> { Data = data, isSuccess = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Product> { Data = null, Message = "Error occurred while creating product.", isSuccess = false };
        }
    }

    public async Task<ServiceResponse<string>> DeleteProduct(Guid id)
    {
        try
        {
            if (_productCache.ContainsKey(id))
            {
                _productCache.Remove(id);
                return new ServiceResponse<string> { Data = "", isSuccess = true };
            }
            else
            {
                return new ServiceResponse<string> { Data = "", Message = "Product not found.", isSuccess = false };
            }
        }
        catch (Exception ex)
        {
            return new ServiceResponse<string> { Data = "", Message = "Error occurred while deleting product.", isSuccess = false };
        }
    }

    public async Task<ServiceResponse<IEnumerable<Product>>> GetAllProducts()
    {
        try
        {
            return new ServiceResponse<IEnumerable<Product>> { Data = _productCache.Values, isSuccess = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<IEnumerable<Product>> { Data = null, Message = "Error occurred while getting products.", isSuccess = false };
        }
    }

    public async Task<ServiceResponse<Product>> GetProductById(Guid id)
    {
        try
        {
            if (_productCache.TryGetValue(id, out Product product))
            {
                return new ServiceResponse<Product> { Data = product, isSuccess = true };
            }
            else
            {
                return new ServiceResponse<Product> { Data = null, Message = $"Product with ID {id} not found.", isSuccess = false };
            }
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Product> { Data = null, Message = $"Error occurred while retrieving product with ID {id}.", isSuccess = false };
        }
    }

    public async Task<ServiceResponse<Product>> UpsertProduct(Product product)
    {
        try
        {
            if (!_productCache.ContainsKey(product.Id))
            {
                _productCache.Add(product.Id, product);
            }
            else
            {
                _productCache[product.Id] = product; // Update the existing product
            }

            return new ServiceResponse<Product> { Data = product, isSuccess = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Product> { Data = null, Message = $"Error occurred while upserting product with ID {product.Id}.", isSuccess = false };
        }
    }
}