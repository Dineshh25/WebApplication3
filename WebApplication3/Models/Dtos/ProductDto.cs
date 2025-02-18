namespace WebApplication3.Models.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
