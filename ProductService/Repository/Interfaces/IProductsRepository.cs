using ProductService.Model;
using ProductService.Model.Request;

namespace ProductService.Repository.Interfaces
{
    public interface IProductsRepository
    {
        Task InsertProduct(Product product);
        Task<Product?> GetProduct(string name);
        Task<List<Product>> GetAllProducts();
    }
}
