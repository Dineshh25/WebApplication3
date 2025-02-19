using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.Dtos;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(CreateProduct createproduct)
        {
            if (createproduct == null)
            {
                return BadRequest();
            }
            try
            {
                var createdproduct = await productService.CreateProductAsync(createproduct);
                return Ok(createdproduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateProductAsync(int Id, UpdateProduct updateproduct)
        {
            if (updateproduct == null)
            {
                return BadRequest();
            }
            try
            {
                var updatedproduct = await productService.UpdateProductAsync(Id, updateproduct);
                return Ok(updatedproduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);

            }
        }

        [HttpDelete]
        [Route("{productId:int}")]
        public async Task<IActionResult> DeleteProductAsync(int productId)
        {
            try
            {
               await productService.DeleteProductAsync(productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }


        [HttpGet("GetproductById")]
        //[Route("{productId:int}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await productService.GetProductByIdAsync(productId);
            return Ok(product);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductAsync(int pageNumber=1)
        {
            int pageSize = 3;
            var product = await productService.GetAllProductsAsync(pageNumber,pageSize);
            return Ok(product);
        }

        [HttpGet]
        [Route("{categoryId:int}")]
        public async Task<IActionResult> GetFilteredProduct(int categoryId)
        {
            var products = await productService.GetFilteredProductAsync(categoryId);
            return Ok(products);
        }
    }

}
