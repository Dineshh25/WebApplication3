using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(CreateProduct createproduct);

        //Task<Product> UpdateProductAsync(int id, UpdateProduct updateproduct);
        //Task DeleteProductAsync(int productId);

        //Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int productId);
    }
}
