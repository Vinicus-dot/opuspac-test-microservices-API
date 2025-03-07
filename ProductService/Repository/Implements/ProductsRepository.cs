using Microsoft.EntityFrameworkCore;
using ProductService.Model;
using ProductService.Repository.Interfaces;

namespace ProductService.Repository.Implements
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductServiceContext _context;
        public ProductsRepository(ProductServiceContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProduct(string name)
        {
           return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task InsertProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
    }
}
