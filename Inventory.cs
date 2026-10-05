namespace InventoryManagementSystem;

/// <summary>
/// Manages the collection of products.
/// Demonstrates abstraction by keeping inventory operations inside one class.
/// </summary>
public class Inventory
{
    private readonly List<Product> products = new();

    public IReadOnlyList<Product> Products => products.AsReadOnly();

    public bool AddProduct(Product product)
    {
        if (products.Any(p => p.Id == product.Id))
            return false;

        products.Add(product);
        return true;
    }

    public Product? FindById(int id)
    {
        return products.FirstOrDefault(p => p.Id == id);
    }

    public List<Product> Search(string keyword)
    {
        keyword = keyword.Trim();

        return products
            .Where(p =>
                p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public bool RemoveProduct(int id)
    {
        Product? product = FindById(id);

        if (product is null)
            return false;

        products.Remove(product);
        return true;
    }

    public List<Product> GetLowStockProducts()
    {
        return products.Where(p => p.IsLowStock()).ToList();
    }

    public decimal GetTotalInventoryValue()
    {
        return products.Sum(p => p.Price * p.Quantity);
    }
}
