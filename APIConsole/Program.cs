using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    // Hardcoded server address
    private static readonly string ServerAddress = "https://localhost:7266/api/";

    static async Task Main(string[] args)
    {
        

        while (true)
        {
            Console.WriteLine("1. Show Server Address");
            Console.WriteLine("2. Get Data from API");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Display the hardcoded server address
                    Console.WriteLine($"Server Address: {ServerAddress}");
                    break;

                case "2":
                    Console.Write("Enter API Endpoint: ");
                    var endpoint = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(endpoint))
                    {
                        var result = await apiService.GetDataFromApi(ServerAddress, endpoint);
                        Console.WriteLine($"API Response: {result}");
                    }
                    else
                    {
                        Console.WriteLine("API Endpoint cannot be empty.");
                    }
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

