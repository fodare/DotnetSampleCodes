using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingExample.Controllers;
using UnitTestingExample.Models;

namespace ProductUnitTest
{
    public class ProductControllerTest
    {
        [Fact]
        public void Test_Controller_Returns_ProductList()
        {
            // Arranage
            Product _product = new Product();
            var _controllerInstance = new ProductController(_product);

            // Act
            var productList = _controllerInstance.Get();
            var result = productList as OkObjectResult;
            var itemCount = (List<Product>?)result?.Value;

            // Assert
            Assert.Equal(4, itemCount?.Count);
        }
        
    }
}
