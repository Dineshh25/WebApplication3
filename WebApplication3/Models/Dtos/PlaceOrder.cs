using System.Text.Json.Serialization;

namespace WebApplication3.Models.Dtos
{
    public class PlaceOrder
    {
        //public required Guid UserId { get; set; }


        //public required int TotalPrice { get; set; }
        //public int ProductId { get; set; }

        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
