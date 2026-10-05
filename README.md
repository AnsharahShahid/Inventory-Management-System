# Inventory Management System

A console-based **Inventory Management System** developed in C# as an Object-Oriented Programming (OOP) Lab project.

## Project Overview

This application helps a small business manage products and inventory through a simple menu-driven console interface.

## Features

- Add new products
- View all products
- Search products by name or category
- Update product information
- Restock products
- Sell products
- Remove products
- Generate a low-stock report
- Calculate total inventory value
- Input validation and error handling
- Sample products included for demonstration

## OOP Concepts Demonstrated

### Encapsulation
Product data is protected through private setters and controlled methods such as `Restock()`, `Sell()`, and `UpdateDetails()`.

### Abstraction
The `Inventory` class manages product collection and inventory operations without exposing its internal list-management logic.

### Classes and Objects
The project uses separate `Product` and `Inventory` classes to model real-world entities.

### Constructors
The `Product` constructor initializes and validates product information.

### Methods
Business operations such as adding, searching, updating, selling, and restocking are implemented through methods.

### Collections
A generic `List<Product>` is used to store and manage inventory items.

### Exception Handling
Invalid operations are handled using validation and `try-catch` blocks.

## Technologies

- C#
- .NET 10
- Object-Oriented Programming
- Console Application
- Visual Studio / VS Code

## How to Run

1. Install the .NET 10 SDK.
2. Clone or download this repository.
3. Open `InventoryManagementSystem.slnx` in Visual Studio.
4. Build the solution.
5. Run the project.

Alternatively, from the project directory:

```bash
dotnet run
```

## Main Menu

```text
1. Add Product
2. View All Products
3. Search Product
4. Update Product
5. Restock Product
6. Sell Product
7. Remove Product
8. Low Stock Report
9. Inventory Summary
0. Exit
```

## Academic Purpose

This project was created for an OOP Lab to demonstrate how object-oriented programming concepts can be applied to a practical inventory-management problem.

## Author

**Ansharah Shahid**

Computer Science Undergraduate
