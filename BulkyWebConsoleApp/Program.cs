using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BulkyWeb.DataAccess;  // Update the namespace if necessary
using BulkyWeb.Models;      // Update the namespace if necessary

class Program
{
    private static ApplicationDbContext _db;

    static void Main(string[] args)
    {
        // Configure the DbContext
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Server=DESKTOP-PSE8GDB;Database=Bulky;Trusted_Connection=True;TrustServerCertificate=True") // Update with your connection string
            .Options;

        _db = new ApplicationDbContext(options);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Insert a new category");
            Console.WriteLine("2. Update an existing category");
            Console.WriteLine("3. Delete a category");
            Console.WriteLine("4. Display all categories");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice (1-5): ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    InsertCategory();
                    break;
                case "2":
                    UpdateCategory();
                    break;
                case "3":
                    DeleteCategory();
                    break;
                case "4":
                    DisplayCategories();
                    break;
                case "5":
                    return;  // Exit the application
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    static void InsertCategory()
    {
        Console.Write("Enter category name: ");
        var name = Console.ReadLine();

        Console.Write("Enter display order: ");
        if (int.TryParse(Console.ReadLine(), out int displayOrder))
        {
            var category = new Category { Name = name, DisplayOrder = displayOrder };
            _db.Categories.Add(category);
            _db.SaveChanges();
            Console.WriteLine("Category inserted.");
        }
        else
        {
            Console.WriteLine("Invalid display order.");
        }
    }

    static void UpdateCategory()
    {
        Console.Write("Enter category ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var category = _db.Categories.Find(id);
            if (category != null)
            {
                Console.Write("Enter new category name: ");
                category.Name = Console.ReadLine();

                Console.Write("Enter new display order: ");
                if (int.TryParse(Console.ReadLine(), out int displayOrder))
                {
                    category.DisplayOrder = displayOrder;
                    _db.Categories.Update(category);
                    _db.SaveChanges();
                    Console.WriteLine("Category updated.");
                }
                else
                {
                    Console.WriteLine("Invalid display order.");
                }
            }
            else
            {
                Console.WriteLine("Category not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    static void DeleteCategory()
    {
        Console.Write("Enter category ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var category = _db.Categories.Find(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
                Console.WriteLine("Category deleted.");
            }
            else
            {
                Console.WriteLine("Category not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    static void DisplayCategories()
    {
        var categories = _db.Categories.ToList();
        if (categories.Any())
        {
            foreach (var genre in categories)
            {
                Console.WriteLine($"Id: {genre.Id}, Name: {genre.Name}, Display Order: {genre.DisplayOrder}");
            }
        }
        else
        {
            Console.WriteLine("No categories found.");
        }
    }
}
