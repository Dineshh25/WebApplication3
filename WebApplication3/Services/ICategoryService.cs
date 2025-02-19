using WebApplication3.Models;
using WebApplication3.Models.Dtos;

namespace WebApplication3.Services
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(CreateCategory createcategory);
        Task<Category> UpdateCategoryAsync(int id, UpdateCategory updatecategory);
        
        Task DeleteCategoryAsync(int categoryId);

        Task<IEnumerable<Category>> GetAllCategoriesAsync(int pageNumber,int pageSize);

        Task<Category> GetCategoryByIdAsync(int categoryId);
    }
}
