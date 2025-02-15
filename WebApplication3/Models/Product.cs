using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }


        public required int Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
