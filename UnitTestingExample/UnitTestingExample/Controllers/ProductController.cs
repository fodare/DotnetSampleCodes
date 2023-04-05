using Microsoft.AspNetCore.Mvc;
using UnitTestingExample.Models;

namespace UnitTestingExample.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductController : ControllerBase
    {
        Product _product = new Product();
        public ProductController(Product product)
        {
            _product = product;
        }

        [HttpGet(Name = "FetchProducts")]
        public IActionResult Get()
        {
            int productListCount = _product.products.Count;
            if (productListCount > 0)
            {
                ;
            }
            else
            {
                _product.SeedProductList();
            }
            var result = _product.GetProducts();
            return Ok(result);
        }

        [HttpPost("Addproduct", Name = "CreateProduct")]
        public IActionResult AddnewProduct(Product newProduct)
        {
            _product.AddProduct(newProduct.Id, newProduct.ProductName);
            var result = _product.GetProducts();
            return Ok(result.ToList());
        }
    }
}
