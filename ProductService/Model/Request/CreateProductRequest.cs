using System.ComponentModel.DataAnnotations;

namespace ProductService.Model.Request
{
    public class CreateProductRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
