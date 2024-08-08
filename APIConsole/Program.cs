using Newtonsoft.Json.Linq;
using ApiService;

class Program
{
    // Hardcoded server address
    private static readonly string ServerAddress = "https://localhost:7266/api";
    private static readonly ApiServices apiService = new ApiServices();

    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Show Server Address");
            Console.WriteLine("2. Get Data from API");
            Console.WriteLine("3. Post Data to API");
            Console.WriteLine("4. Put Data to API (PUT)");
            Console.WriteLine("5. Delete Data from API (DELETE)");
            Console.WriteLine("6. Exit");
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
                    var getEndpoint = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(getEndpoint))
                    {
                        var result = await apiService.GetDataFromApi(ServerAddress, getEndpoint);
                        Console.WriteLine($"{result}");
                    }
                    else
                    {
                        Console.WriteLine("API Endpoint cannot be empty.");
                    }
                    break;

                case "3":
                    Console.Write("Enter API Endpoint for POST: ");
                    var postEndpoint = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(postEndpoint))
                    {
                        Console.WriteLine("Enter the data to send (JSON format):");
                        var jsonData = Console.ReadLine();
                        try
                        {
                            var postData = JObject.Parse(jsonData);
                            var result = await apiService.PostDataToApi(ServerAddress, postEndpoint, postData);
                            Console.WriteLine($"API Response (POST): {result}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error parsing JSON: {e.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("API Endpoint cannot be empty.");
                    }
                    break;

                case "4":
                    Console.Write("Enter API Endpoint for PUT: ");
                    var putEndpoint = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(putEndpoint))
                    {
                        Console.WriteLine("Enter the data to send (JSON format):");
                        var jsonData = Console.ReadLine();
                        try
                        {
                            var putData = JObject.Parse(jsonData);
                            var result = await apiService.PutDataToApi(ServerAddress, putEndpoint, putData);
                            Console.WriteLine($"API Response (PUT): {result}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error parsing JSON: {e.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("API Endpoint cannot be empty.");
                    }
                    break;

                case "5":
                    Console.Write("Enter API Endpoint for DELETE: ");
                    var deleteEndpoint = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(deleteEndpoint))
                    {
                        var result = await apiService.DeleteDataFromApi(ServerAddress, deleteEndpoint);
                        Console.WriteLine($"API Response (DELETE): {result}");
                    }
                    else
                    {
                        Console.WriteLine("API Endpoint cannot be empty.");
                    }
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
