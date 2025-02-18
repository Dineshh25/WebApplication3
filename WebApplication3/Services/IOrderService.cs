using System;
using WebApplication3.Models;
using WebApplication3.Models.Dtos;

namespace WebApplication3.Services
{
    public interface IOrderService
    {
       Task<ICollection<Order>> GetAllOrdersAsync(int pageNumber, int pageSize);

         Task<Order> GetOrderByIdAsync(int orderId);

        Task<Order> PlaceOrderAsync(Guid userId,PlaceOrder placeorder);
      

        Task<Order> UpdateOrderAsync(int orderId,UpdateOrder updateorder);

        Task DeleteOrderAsync(int orderId);


    }
}
