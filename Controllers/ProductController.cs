using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GlobalExceptionHandling.Models;
using NotImplementedException = GlobalExceptionHandling.Exceptions.NotImplementedException;
using GlobalExceptionHandling.Exceptions;

namespace GlobalExceptionHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private ILogger<ProductController> _logger;
        public ProductController(IProductService productService,ILogger<ProductController> logger)
        {
            _productService= productService;
            _logger= logger;
        }
        [HttpGet("productlist")]
        public Task<IEnumerable<Product>> ProductList()
        {
            var productList = _productService.GetProductList();
            return productList;
        }
        [HttpGet("getproductbyid")]
        public Task<Product> GetProductById(int Id)
        {
            _logger.LogInformation($"Fetch Product with ID: {Id} from the database");
            var product = _productService.GetProductById(Id);
            if (product.Result == null)
            {
                throw new NotFoundException($"Product ID {Id} not found.");
            }
            _logger.LogInformation($"Returning product with ID: {product.Result.ProductId}.");
            return product;
        }
        [HttpPost("addproduct")]
        public Task<Product> AddProduct(Product product)
        {
            return _productService.AddProduct(product);
        }
        [HttpPut("updateproduct")]
        public Task<Product> UpdateProduct(Product product)
        {
            return _productService.UpdateProduct(product);
        }
        [HttpDelete("deleteproduct")]
        public Task<bool> DeleteProduct(int Id)
        {
            return _productService.DeleteProduct(Id);
        }
        [HttpGet("filterproduct")]
        public Task<List<Product>> FilterProduct(int Id)
        {
            throw new NotImplementedException("Not Implemented Exception!");
        }
    }
}
