using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Model.Entity
{
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("message")]
        public string Message { get; set; }
    }
}
