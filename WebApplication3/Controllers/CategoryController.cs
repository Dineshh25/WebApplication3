using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.Dtos;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(CreateCategory createcategory)
        {
            if (createcategory == null)
            {
                return BadRequest(new { message = "Invalid category data." });
            }
            try
            {
                var createdcategory = await categoryService.CreateCategoryAsync(createcategory);
                return Ok(createdcategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCategoryAsync(int id, UpdateCategory updatecategory)
        {

            if (updatecategory == null)
            {
                return NotFound();
            }
            try
            {
                var updatedCategory = await categoryService.UpdateCategoryAsync(id, updatecategory);
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{categoryId:int}")]
        public async Task<IActionResult> DeleteCategoryAsync(int categoryId)
        {

            try
            {
                await categoryService.DeleteCategoryAsync(categoryId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{categoryId:int}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int categoryId)
        {
            var category = await categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
    }
}
