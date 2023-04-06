namespace UnitTestingExample.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }

        public List<Product> products = new List<Product>();

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

        public string GetProductName(int id)
        {
            var product = products.Where(x => x.Id == id);
            return product.FirstOrDefault()?.ProductName;
        }

        public string UpdateProductName(int id, string productName)
        {
            int itemIndex = products.FindIndex(x => x.Id == id);
            var result = "";
            if (itemIndex != -1)
            {
                products[itemIndex].ProductName = productName;
                result = "Okay. Item name modified!";
            }
            else
            {
                result = "Error modifying item!";
            }
            return result;
        }

        public string RemoveItem(int id)
        {
            var result = "";
            int itemIndex = products.FindIndex(x => x.Id == id);
            if (itemIndex != -1)
            {
                products.RemoveAt(itemIndex);
                return "Ok. Item removed";
            }
            else
            {
                return "Error removing item!";
            }
        }
    }
}
