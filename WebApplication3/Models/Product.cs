using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication3.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }


        public required int Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

        [ForeignKey("Category")]
        public  required int CategoryId { get; set; }

     



        //[JsonIgnore]
        public Category Category { get; set; }

        //[JsonIgnore]
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
