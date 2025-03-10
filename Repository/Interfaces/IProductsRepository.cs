using Model;
using Model.Product.Entity;

namespace Repository.Interfaces
{
    public interface IProductsRepository
    {
        Task InsertProduct(Product product);
        Task<Product?> GetProduct(string name);
        Task<ListResponse<Product>> GetAllProducts(int pageNumber, int pageSize);
    }
}
