using InventoryManagementSystem;

Inventory inventory = new();

SeedSampleData();

while (true)
{
    Console.Clear();
    PrintHeader();
    PrintMenu();

    Console.Write("Enter your choice: ");
    string choice = Console.ReadLine()?.Trim() ?? "";

    try
    {
        switch (choice)
        {
            case "1":
                AddProduct();
                break;
            case "2":
                ViewProducts();
                break;
            case "3":
                SearchProducts();
                break;
            case "4":
                UpdateProduct();
                break;
            case "5":
                RestockProduct();
                break;
            case "6":
                SellProduct();
                break;
            case "7":
                RemoveProduct();
                break;
            case "8":
                ShowLowStock();
                break;
            case "9":
                ShowInventoryValue();
                break;
            case "0":
                Console.WriteLine("\nThank you for using the Inventory Management System.");
                return;
            default:
                Pause("Invalid choice. Please select an option from 0 to 9.");
                break;
        }
    }
    catch (Exception ex)
    {
        Pause($"Error: {ex.Message}");
    }
}

void AddProduct()
{
    Console.Clear();
    PrintSection("ADD NEW PRODUCT");

    int id = ReadPositiveInt("Product ID: ");

    if (inventory.FindById(id) is not null)
    {
        Pause("A product with this ID already exists.");
        return;
    }

    string name = ReadRequiredText("Product name: ");
    string category = ReadRequiredText("Category: ");
    decimal price = ReadNonNegativeDecimal("Price: ");
    int quantity = ReadNonNegativeInt("Initial quantity: ");
    int reorderLevel = ReadNonNegativeInt("Reorder level: ");

    Product product = new(id, name, category, price, quantity, reorderLevel);

    if (inventory.AddProduct(product))
        Pause("Product added successfully.");
    else
        Pause("Product could not be added.");
}

void ViewProducts()
{
    Console.Clear();
    PrintSection("ALL PRODUCTS");

    if (inventory.Products.Count == 0)
    {
        Pause("No products found.");
        return;
    }

    PrintProductTable(inventory.Products);
    Pause();
}

void SearchProducts()
{
    Console.Clear();
    PrintSection("SEARCH PRODUCTS");

    string keyword = ReadRequiredText("Enter product name or category: ");
    List<Product> results = inventory.Search(keyword);

    if (results.Count == 0)
    {
        Pause("No matching products found.");
        return;
    }

    PrintProductTable(results);
    Pause();
}

void UpdateProduct()
{
    Console.Clear();
    PrintSection("UPDATE PRODUCT");

    int id = ReadPositiveInt("Enter product ID: ");
    Product? product = inventory.FindById(id);

    if (product is null)
    {
        Pause("Product not found.");
        return;
    }

    Console.WriteLine($"Current product: {product.Name}");
    string name = ReadRequiredText("New name: ");
    string category = ReadRequiredText("New category: ");
    decimal price = ReadNonNegativeDecimal("New price: ");
    int reorderLevel = ReadNonNegativeInt("New reorder level: ");

    product.UpdateDetails(name, category, price, reorderLevel);
    Pause("Product updated successfully.");
}

void RestockProduct()
{
    Console.Clear();
    PrintSection("RESTOCK PRODUCT");

    int id = ReadPositiveInt("Enter product ID: ");
    Product? product = inventory.FindById(id);

    if (product is null)
    {
        Pause("Product not found.");
        return;
    }

    int amount = ReadPositiveInt("Quantity to add: ");
    product.Restock(amount);

    Pause($"Restocked successfully. New quantity: {product.Quantity}");
}

void SellProduct()
{
    Console.Clear();
    PrintSection("SELL PRODUCT");

    int id = ReadPositiveInt("Enter product ID: ");
    Product? product = inventory.FindById(id);

    if (product is null)
    {
        Pause("Product not found.");
        return;
    }

    int amount = ReadPositiveInt("Quantity to sell: ");

    if (product.Sell(amount))
        Pause($"Sale completed. Remaining quantity: {product.Quantity}");
    else
        Pause("Sale failed. Quantity is not available.");
}

void RemoveProduct()
{
    Console.Clear();
    PrintSection("REMOVE PRODUCT");

    int id = ReadPositiveInt("Enter product ID: ");
    Product? product = inventory.FindById(id);

    if (product is null)
    {
        Pause("Product not found.");
        return;
    }

    Console.Write($"Remove '{product.Name}'? (Y/N): ");
    string confirmation = Console.ReadLine()?.Trim().ToUpperInvariant() ?? "";

    if (confirmation == "Y")
    {
        inventory.RemoveProduct(id);
        Pause("Product removed successfully.");
    }
    else
    {
        Pause("Operation cancelled.");
    }
}

void ShowLowStock()
{
    Console.Clear();
    PrintSection("LOW STOCK REPORT");

    List<Product> lowStock = inventory.GetLowStockProducts();

    if (lowStock.Count == 0)
    {
        Pause("No products are currently at or below their reorder level.");
        return;
    }

    PrintProductTable(lowStock);
    Pause();
}

void ShowInventoryValue()
{
    Console.Clear();
    PrintSection("INVENTORY SUMMARY");

    Console.WriteLine($"Total products : {inventory.Products.Count}");
    Console.WriteLine($"Low-stock items: {inventory.GetLowStockProducts().Count}");
    Console.WriteLine($"Inventory value: {inventory.GetTotalInventoryValue():C}");

    Pause();
}

void PrintProductTable(IEnumerable<Product> products)
{
    Console.WriteLine();
    Console.WriteLine($"{"ID",-5} {"Name",-24} {"Category",-18} {"Price",10} {"Qty",8} {"Reorder",8}");
    Console.WriteLine(new string('-', 80));

    foreach (Product product in products)
        Console.WriteLine(product);

    Console.WriteLine(new string('-', 80));
}

void PrintMenu()
{
    Console.WriteLine("1. Add Product");
    Console.WriteLine("2. View All Products");
    Console.WriteLine("3. Search Product");
    Console.WriteLine("4. Update Product");
    Console.WriteLine("5. Restock Product");
    Console.WriteLine("6. Sell Product");
    Console.WriteLine("7. Remove Product");
    Console.WriteLine("8. Low Stock Report");
    Console.WriteLine("9. Inventory Summary");
    Console.WriteLine("0. Exit");
    Console.WriteLine();
}

void PrintHeader()
{
    Console.WriteLine("==============================================");
    Console.WriteLine("       INVENTORY MANAGEMENT SYSTEM");
    Console.WriteLine("==============================================");
    Console.WriteLine();
}

void PrintSection(string title)
{
    Console.WriteLine("==============================================");
    Console.WriteLine($"              {title}");
    Console.WriteLine("==============================================");
    Console.WriteLine();
}

void Pause(string message = "Press Enter to continue...")
{
    Console.WriteLine();
    Console.WriteLine(message);
    Console.ReadLine();
}

string ReadRequiredText(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string value = Console.ReadLine()?.Trim() ?? "";

        if (!string.IsNullOrWhiteSpace(value))
            return value;

        Console.WriteLine("This field cannot be empty.");
    }
}

int ReadPositiveInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);

        if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
            return value;

        Console.WriteLine("Please enter a whole number greater than 0.");
    }
}

int ReadNonNegativeInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);

        if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
            return value;

        Console.WriteLine("Please enter a whole number 0 or greater.");
    }
}

decimal ReadNonNegativeDecimal(string prompt)
{
    while (true)
    {
        Console.Write(prompt);

        if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= 0)
            return value;

        Console.WriteLine("Please enter a valid number 0 or greater.");
    }
}

void SeedSampleData()
{
    inventory.AddProduct(new Product(101, "Wireless Mouse", "Electronics", 2500m, 15, 5));
    inventory.AddProduct(new Product(102, "Keyboard", "Electronics", 4200m, 8, 3));
    inventory.AddProduct(new Product(103, "Notebook", "Stationery", 450m, 25, 10));
    inventory.AddProduct(new Product(104, "USB Cable", "Accessories", 900m, 4, 5));
}
