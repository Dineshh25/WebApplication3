using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Models.Dtos;

namespace WebApplication3.Services
{
    public class OrderService : IOrderService

    {
        private ApplicationDbContext dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;   
        }

        public async Task<Order> PlaceOrderAsync(Guid userId,PlaceOrder placeorder)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);
            if (user == null)
            {
                return null;
            }

            var product = await dbContext.Products.FindAsync(placeorder.ProductId);
            if (product == null)
            {
                return null; 
            }
            int totalPrice = product.Price * placeorder.Quantity;

            var order = new Order
            {
                UserId = userId,
                IsDeleted = false,
                Quantity = placeorder.Quantity,
                ProductId = placeorder.ProductId,
                TotalPrice = totalPrice    
            };

           
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId && !o.IsDeleted);

        }

        public async Task<ICollection<Order>> GetAllOrdersAsync(int pageNumber, int pageSize)
        {
            return await dbContext.Orders
                                  .Where(o => !o.IsDeleted)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
        }

       

        public async Task<Order> UpdateOrderAsync(int orderId, UpdateOrder updateorder)
        {
            var existingorder = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId && !o.IsDeleted);
            if (existingorder == null)
            {
                return null;
            }

            var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == updateorder.ProductId);
            if (product == null)
            {
                return null; 
            }

            existingorder.Quantity = updateorder.Quantity;
            existingorder.ProductId = updateorder.ProductId;


            existingorder.TotalPrice = existingorder.Quantity * product.Price;
            dbContext.Orders.Update(existingorder);
            await dbContext.SaveChangesAsync();
                     
            return existingorder;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            if(order != null)
            {
                order.IsDeleted = true;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
