using ProductService.Model.DTO;
using ProductService.Model.Entity;
using ProductService.Model.Request;

namespace ProductService.Business.Interfaces
{
    public interface IProductsBusiness
    {
        Task<object?> CreateProduct(CreateProductRequest createProductRequest);
        Task<List<ProductDTO>> GetAllProducts();
    }
}
