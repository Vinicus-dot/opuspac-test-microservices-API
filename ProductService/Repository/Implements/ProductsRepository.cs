using Microsoft.EntityFrameworkCore;
using Model;
using ProductService.Model.Entity;
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

        public async Task<ListResponse<Product>> GetAllProducts(int pageNumber, int pageSize)
        {
            var products = await _context.Products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new ListResponse<Product>
            {
                Data = products,
                Total = await _context.Products.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
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
