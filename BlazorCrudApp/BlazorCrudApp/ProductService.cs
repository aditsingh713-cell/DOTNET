namespace BlazorCrudApp;

public class ProductService
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Gaming Mouse", Price = 59.99m },
        new Product { Id = 2, Name = "Mechanical Keyboard", Price = 120.50m }
    };

    // 1. READ ALL (This fixes your current error!)
    public List<Product> GetProducts()
    {
        return _products;
    }

    // 2. READ SINGLE
    public Product? GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    // 3. CREATE / UPDATE
    public void SaveProduct(Product product)
    {
        if (product.Id == 0)
        {
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
        }
        else
        {
            var existing = GetProductById(product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
            }
        }
    }

    // 4. DELETE
    public void DeleteProduct(int id)
    {
        var product = GetProductById(id);
        if (product != null)
        {
            _products.Remove(product);
        }
    }
}