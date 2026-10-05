namespace InventoryManagementSystem;

/// <summary>
/// Represents a product stored in the inventory.
/// Demonstrates encapsulation through private setters and validation.
/// </summary>
public class Product
{
    public int Id { get; }
    public string Name { get; private set; }
    public string Category { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public int ReorderLevel { get; private set; }

    public Product(int id, string name, string category, decimal price, int quantity, int reorderLevel)
    {
        if (id <= 0) throw new ArgumentException("ID must be greater than 0.");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Product name is required.");
        if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Category is required.");
        if (price < 0) throw new ArgumentException("Price cannot be negative.");
        if (quantity < 0) throw new ArgumentException("Quantity cannot be negative.");
        if (reorderLevel < 0) throw new ArgumentException("Reorder level cannot be negative.");

        Id = id;
        Name = name.Trim();
        Category = category.Trim();
        Price = price;
        Quantity = quantity;
        ReorderLevel = reorderLevel;
    }

    public void UpdateDetails(string name, string category, decimal price, int reorderLevel)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Product name is required.");
        if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Category is required.");
        if (price < 0) throw new ArgumentException("Price cannot be negative.");
        if (reorderLevel < 0) throw new ArgumentException("Reorder level cannot be negative.");

        Name = name.Trim();
        Category = category.Trim();
        Price = price;
        ReorderLevel = reorderLevel;
    }

    public void Restock(int amount)
    {
        if (amount <= 0) throw new ArgumentException("Restock amount must be greater than 0.");
        Quantity += amount;
    }

    public bool Sell(int amount)
    {
        if (amount <= 0) return false;
        if (amount > Quantity) return false;

        Quantity -= amount;
        return true;
    }

    public bool IsLowStock() => Quantity <= ReorderLevel;

    public override string ToString()
    {
        return $"{Id,-5} {Name,-24} {Category,-18} {Price,10:C} {Quantity,8} {ReorderLevel,8}";
    }
}
