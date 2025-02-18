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
            var user = await dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return null;
            }

            var order = new Order
            {
                UserId = userId,
                IsDeleted = false,
                OrderItems = new List<OrderItem>()
            };

            foreach(var items in placeorder.OrderItems)
            {
                  var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == items.ProductId);

                var orderitem = new OrderItem
                {
                    ProductId = items.ProductId,
                    Quantity = items.Quantity,
                    Product = product
                };
                order.TotalPrice += product.Price * items.Quantity;
                order.OrderItems.Add(orderitem);
            }
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
            
            var orders = await dbContext.Orders
                .Where(o => !o.IsDeleted)
                .Include(o => o.OrderItems)
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();

            return orders;
            
        }

       

        public async Task<Order> UpdateOrderAsync(int orderId, UpdateOrder updateorder)
        {
            var existingorder = await dbContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);
            if (existingorder == null)
            {
                return null;
            }
              
            foreach (var item in updateorder.OrderItems)
            {
                var product = existingorder.OrderItems.FirstOrDefault(oi => oi.ProductId == item.ProductId);
                if (product != null)
                {
                    product.Quantity = item.Quantity;
                    product.ProductId = item.ProductId;
                    dbContext.Entry(product).State = EntityState.Modified;
                }
                else
                {
                    existingorder.OrderItems.Add(new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        OrderId = existingorder.Id
                    });
                }
                dbContext.Orders.Update(existingorder);
                await dbContext.SaveChangesAsync();


            }

            
           
            return existingorder;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            {
                order.IsDeleted = true;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
