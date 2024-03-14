using GlobalExceptionHandling.Data;
using Microsoft.EntityFrameworkCore;

namespace GlobalExceptionHandling.Models
{
    public class ProductServicecs:IProductService
    {
        private readonly DbContextClass _dbContext;
        public ProductServicecs(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Product>> GetProductList()
        {
            return await _dbContext.products.ToListAsync();
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _dbContext.products.FirstOrDefaultAsync(e=>e.ProductId==id);   
        }
        public async Task<Product> AddProduct(Product product)
        {
            _dbContext.products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
        public async Task<Product> UpdateProduct(Product product)
        {
            var result=await _dbContext.products.FirstOrDefaultAsync(e=>e.ProductId == product.ProductId);
            if (result != null)
            {
                result.ProductId= product.ProductId;
                result.ProductName= product.ProductName;
                result.ProductDescription= product.ProductDescription;
                result.ProductPrice= product.ProductPrice;
                result.ProductStock= product.ProductStock;
            }
            await _dbContext.SaveChangesAsync();
            return result;
        }
        public async Task<bool> DeleteProduct(int Id)
        {
            var result= await _dbContext.products.FirstOrDefaultAsync(e=> e.ProductId==Id);
            if(result != null)
            {
                _dbContext.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
    }
}
