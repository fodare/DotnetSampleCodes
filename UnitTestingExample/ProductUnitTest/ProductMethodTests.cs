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

        [Theory]
        [InlineData("SampleProduct")]
        public void Test_Update_ItemName_Method(string productName)
        {
            // Arranage
            Product _product = new Product();
            _product.SeedProductList();
            int testId = 1;
            string oldProductname = _product.GetProductName(testId);

            // Act
            _product.UpdateProductName(testId, productName);
            string newProdcutName = _product.GetProductName(testId);

            // Arrange
            Assert.Equal(newProdcutName, productName);
        }
    }
}