namespace WebApplication3.Models.Dtos
{
    public class UpdateProduct
    {
        public required string Name { get; set; }

        public required int Price { get; set; }

        public required int CategoryId { get; set; }


    }
}
