using System.Text.Json.Serialization;

namespace WebApplication3.Models.Dtos
{
    public class PlaceOrder
    {
        public required int Quantity { get; set; }


        //public required int TotalPrice { get; set; }
        public int ProductId { get; set; }


    }
}
