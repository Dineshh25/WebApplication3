using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
