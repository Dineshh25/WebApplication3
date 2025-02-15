namespace WebApplication3.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public bool IsDeleted { get; set; }


    }
}
