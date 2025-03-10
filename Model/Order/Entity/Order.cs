using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Order.Entity
{
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("message")]
        public string Message { get; set; }
    }
}
