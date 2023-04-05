using UnitTestingExample.Models;

namespace ProductUnitTest
{
    public class ProductMethodTests
    {
        [Fact]
        public void SeedProduct_Adds_4_items()
        {
            // Arrange
            Product _product = new Product();

            // Act 
            _product.SeedProductList();
            var productItemCount = _product.GetProducts().Count;    

            // Assert
            Assert.Equal(4, productItemCount);
        }
    }
}