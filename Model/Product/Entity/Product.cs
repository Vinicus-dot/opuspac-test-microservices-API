using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Product.Entity
{
    public class Product
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("price")]
        public decimal Price { get; set; } 
    }
}

