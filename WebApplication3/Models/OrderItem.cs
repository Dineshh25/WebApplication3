using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication3.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        //public int Price { get; set; }

        [ForeignKey ("Order")]
        public int OrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }




    }
}
