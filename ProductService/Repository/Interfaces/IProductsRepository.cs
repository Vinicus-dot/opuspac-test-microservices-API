using ProductService.Model.Entity;
using ProductService.Model.Request;
using ProductService.Model.Response;

namespace ProductService.Repository.Interfaces
{
    public interface IProductsRepository
    {
        Task InsertProduct(Product product);
        Task<Product?> GetProduct(string name);
        Task<ListResponse<Product>> GetAllProducts(int pageNumber, int pageSize);
    }
}
