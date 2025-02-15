using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class CategoryService : ICategoryService
    {
      
        private ApplicationDbContext dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await dbContext.Categories.Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await dbContext.Categories.FindAsync(categoryId);
        }

        public async Task<Category> CreateCategoryAsync(CreateCategory createcategory)
        {
            var category = new Category
            {
                Name = createcategory.Name,
                IsDeleted = false
            };
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateCategoryAsync(int id, UpdateCategory updatecategory)
        {
            var existingCategory = await dbContext.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = updatecategory.Name;
            //existingCategory.IsDeleted = updatecategory.IsDeleted;

            dbContext.Categories.Update(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await dbContext.Categories.FindAsync(categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                await dbContext.SaveChangesAsync(); ;
            }
            
        }
    }
}
