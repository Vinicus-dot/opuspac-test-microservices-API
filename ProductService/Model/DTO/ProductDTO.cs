using ProductService.Model.Entity;

namespace ProductService.Model.DTO
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ProductDTO() { }

        public ProductDTO(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
        }

        public List<ProductDTO> ToListDto(IEnumerable<Product> products)
        {
            ArgumentNullException.ThrowIfNull(products);
            return products.Select(p => new ProductDTO(p)).ToList();
        }
    }

}
