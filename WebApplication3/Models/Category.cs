namespace WebApplication3.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public bool IsDeleted { get; set; }

    }
}
