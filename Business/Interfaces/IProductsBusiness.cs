using Model;
using Model.Product.DTO;
using Model.Product.Request;

namespace ProductService.Business.Interfaces
{
    public interface IProductsBusiness
    {
        Task<object?> CreateProduct(CreateProductRequest createProductRequest);
        Task<ListResponse<ProductDTO>> GetAllProducts(int pageNumber, int pageSize);
    }
}
