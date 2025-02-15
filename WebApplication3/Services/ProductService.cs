using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;
        public ProductService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await dbContext.Products.FindAsync(productId);
        }

        public async Task<Product> CreateProductAsync(CreateProduct createproduct)
        {
            var product = new Product
            {
                Name = createproduct.Name,
                Price = createproduct.Price,
                //CategoryId = createproduct.CategoryId,
                IsDeleted = false
            };
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(int id, UpdateProduct updateproduct)
        {
            var existingProduct = await dbContext.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.Name = updateproduct.Name;
            existingProduct.Price = updateproduct.Price;

            dbContext.Products.Update(existingProduct);
            await dbContext.SaveChangesAsync();
            return existingProduct;
        }
    }
}
