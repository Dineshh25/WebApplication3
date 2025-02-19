    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication3.Models.Dtos;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService orderService;


        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }




        [HttpPost]
        [Route("{userId:guid}")]
        public async Task<IActionResult> PlaceOrderAsync(Guid userId, PlaceOrder placeorder)
        {
            if (placeorder == null)
            {
                return BadRequest();
            }
            try
            {
                var placedorder = await orderService.PlaceOrderAsync(userId, placeorder);
                return Ok(placedorder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync(int pageNumber =1)
        {
            int pageSize = 3;
            var orders = await orderService.GetAllOrdersAsync(pageNumber,pageSize);
            return Ok(orders);
        }

        [HttpGet]
        [Route("{orderId:int}")]
        public async Task<IActionResult> GetOrderByIdAsync(int orderId)
        {
            var order = await orderService.GetOrderByIdAsync(orderId);
            return Ok(order);
        }

        [HttpDelete]
        [Route("{orderId:int}")]
        public async Task<IActionResult> DeleteOrderAsync(int orderId)
        {
            try
            {
                await orderService.DeleteOrderAsync(orderId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("{orderId:int}")]
        public async Task<IActionResult> UpdateOrderAsync(int orderId, UpdateOrder updateorder)
        {
            if (updateorder == null)
            {
                return BadRequest();
            }
            try
            {
                var updatedorder = await orderService.UpdateOrderAsync(orderId, updateorder);
                return Ok(updatedorder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
