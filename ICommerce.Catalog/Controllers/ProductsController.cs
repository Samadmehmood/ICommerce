using ICommerce.Contracts;
using ICommerce.Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using ICommerce.Catalog.Services;
using AutoMapper;

namespace ICommerce.Catalog.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productsService;
    private readonly IMapper _mapper;
    public ProductsController(IProductService productsService, IMapper mapper)
    {
        _productsService = productsService;
        _mapper=mapper;
    }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="request">The request containing product details.</param>
        /// <returns>The created product.</returns>
  [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        try
        {
            var product = new Product(
                Guid.NewGuid(),
                request.title,
                request.description,
                DateTime.UtcNow,
                DateTime.UtcNow,
                request.manufacturedAt
            );

            var serviceResponse = await _productsService.CreateProduct(product);

            if (!serviceResponse.isSuccess)
            {
                return HandleServiceFailure(serviceResponse);
            }

            var response = new ApiResponse<ProductResponse>
            {
                StatusCode = 201,
                Message = "Successfully Created Product.",
                Data = _mapper.Map<ProductResponse>(serviceResponse.Data)
            };

            return new JsonResult(response) { StatusCode = response.StatusCode };
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
        /// Retrieves all products
        /// </summary>
        /// <returns>Products List</returns>
    [HttpGet("GetAll")]
public async Task<IActionResult> GetAllProducts()
{
    try
    {
        var serviceResponse = await _productsService.GetAllProducts();

        if (!serviceResponse.isSuccess)
        {
            return HandleServiceFailure(serviceResponse);
        }

        var response = new ApiResponse<IEnumerable<ProductResponse>>
        {
            StatusCode = 200,
            Message = "Products retrieved successfully.",
            Data = _mapper.Map<IEnumerable<ProductResponse>>(serviceResponse.Data)
        };

        return new JsonResult(response) { StatusCode = response.StatusCode };
    }
    catch (Exception ex)
    {
        return HandleException(ex);
    }
}

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product details.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        try
        {
            var serviceResponse = await _productsService.GetProductById(id);

            if (!serviceResponse.isSuccess)
            {
                return HandleServiceFailure(serviceResponse);
            }

            var response = new ApiResponse<ProductResponse>
            {
                StatusCode = 200,
                Message = "Product found.",
                Data = _mapper.Map<ProductResponse>(serviceResponse.Data)
            };

            return new JsonResult(response) { StatusCode = response.StatusCode };
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
        /// <summary>
        /// Updates or creates a product.
        /// </summary>
        /// <param name="request">The request containing product details.</param>
        /// <returns>The updated or created product.</returns>
    [HttpPut]
    public async Task<IActionResult> UpsertProduct(UpsertProductRequest request)
    {
        try
        {
            var product = new Product(
                request.id,
                request.title,
                request.description,
                DateTime.UtcNow,
                DateTime.UtcNow,
                request.manufacturedAt
            );

            var serviceResponse = await _productsService.UpsertProduct(product);

            if (!serviceResponse.isSuccess)
            {
                return HandleServiceFailure(serviceResponse);
            }

            var response = new ApiResponse<ProductResponse>
            {
                StatusCode = 200, 
                Message = "Product Updated successfully.",
                Data = _mapper.Map<ProductResponse>(serviceResponse.Data)
            };

            return new JsonResult(response) { StatusCode = response.StatusCode };
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A message indicating the result of the operation.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var serviceResponse = await _productsService.DeleteProduct(id);

            if (!serviceResponse.isSuccess)
            {
                return HandleServiceFailure(serviceResponse);
            }

            var response = new ApiResponse<object>
            {
                StatusCode = 200,
                Message = "Product deleted successfully.",
                Data = null
            };

            return new JsonResult(response) { StatusCode = response.StatusCode };
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

[ApiExplorerSettings(IgnoreApi = true)]
    private IActionResult HandleServiceFailure<T>(ServiceResponse<T> serviceResponse)
    {
        var errorResponse = new ApiResponse<object>
        {
            StatusCode = 500,
            Message = serviceResponse.Message,
            Data = null
        };

        return new JsonResult(errorResponse) { StatusCode = errorResponse.StatusCode };
    }
[ApiExplorerSettings(IgnoreApi = true)]
    private IActionResult HandleException(Exception ex)
    {
        var errorResponse = new ApiResponse<object>
        {
            StatusCode = 500,
            Message = "An error occurred while processing the request.",
            Data = null
        };
        return new JsonResult(errorResponse) { StatusCode = errorResponse.StatusCode };
    }
}