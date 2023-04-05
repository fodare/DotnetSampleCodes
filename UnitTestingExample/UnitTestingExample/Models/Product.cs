namespace UnitTestingExample.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }

        public IList<Product> products = new List<Product>();

        public void SeedProductList()
        {
            products.Add(new Product { Id = 1, ProductName = "Wireless Keyboard" });
            products.Add(new Product { Id = 2, ProductName = "Logitech mice" });
            products.Add(new Product { Id = 3, ProductName = "Fidget spinner" });
            products.Add(new Product { Id = 4, ProductName = "Wireless speaker" });
        }

        public void AddProduct(int id, string productName)
        {
            products.Add(new Product { Id = id, ProductName = productName });
        }

        public List<Product> GetProducts()
        {
            return products.ToList();
        }
    }
}
