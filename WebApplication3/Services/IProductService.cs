using WebApplication3.Models;
using WebApplication3.Models.Dtos;

namespace WebApplication3.Services
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(CreateProduct createproduct);

        Task<Product> UpdateProductAsync(int id, UpdateProduct updateproduct);
        Task DeleteProductAsync(int productId);

        Task<ICollection<ProductDto>> GetAllProductsAsync(int pageNumber, int pageSize);

        Task<ProductDto> GetProductByIdAsync(int productId);

        Task<ICollection<ProductDto>> GetFilteredProductAsync(int categoryId);
    }
}
