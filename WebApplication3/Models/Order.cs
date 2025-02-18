using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication3.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

       

        
        [JsonIgnore]
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }

   
}
