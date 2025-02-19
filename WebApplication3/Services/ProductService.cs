using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Models.Dtos;

namespace WebApplication3.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;
        public ProductService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


       

        public async Task<Product> CreateProductAsync(CreateProduct createproduct)
        {
            var product = new Product
            {
                Name = createproduct.Name,
                Price = createproduct.Price,
                CategoryId = createproduct.CategoryId,
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
            existingProduct.CategoryId = updateproduct.CategoryId;
            
            dbContext.Products.Update(existingProduct);
            await dbContext.SaveChangesAsync();
            return existingProduct;
        }

       
        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            return await dbContext.Products
                .Where(p => p.Id == productId && !p.IsDeleted)
                .Include(p => p.Category)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CreatedAt = p.CreatedAt,
                    IsDeleted = p.IsDeleted,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name

                })
                .FirstOrDefaultAsync();
        }

      
        public async Task<ICollection<ProductDto>> GetAllProductsAsync(int pageNumber, int pageSize )
        {
            

            var products = await dbContext.Products
                .Where(p=> !p.IsDeleted)
                .Include(p => p.Category)
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CreatedAt = p.CreatedAt,
                    IsDeleted = p.IsDeleted,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name
                })
                .ToListAsync();

            return products;
            
           
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await dbContext.Products.FindAsync(productId);
            if (product != null)
            {
                product.IsDeleted = true;
                await dbContext.SaveChangesAsync();
            } 


        }

        public async Task<ICollection<ProductDto>> GetFilteredProductAsync(int categoryId)
        {
            var products = await dbContext.Products
                                .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
                                .Select(p => new ProductDto
                                {
                                    Id = p.Id,
                                    Name = p.Name,
                                    Price = p.Price,
                                    CreatedAt = p.CreatedAt,
                                    IsDeleted = p.IsDeleted,
                                    CategoryId = p.CategoryId,
                                    CategoryName = p.Category.Name
                                })
                               .ToListAsync();
                     return products;

        }
        
    }
}
