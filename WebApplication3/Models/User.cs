using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public required string Username { get; set; }

        public required string Email { get; set; }

        public DateTime CreatedAt { get; set; }= DateTime.Now;

        public bool IsDeleted { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();




    }
}
