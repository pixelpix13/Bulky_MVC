using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    // Hardcoded server address
    private static readonly string ServerAddress = "https://localhost:7266/api";

    static async Task Main(string[] args)
    {
        var apiService = new ApiService();

        while (true)
        {
            Console.WriteLine("1. Show Server Address");
            Console.WriteLine("2. Get Data from API");
            Console.WriteLine("3. Post Data to API");
            Console.WriteLine("4. Exit");
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
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<JToken> GetDataFromApi(string baseUrl, string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{baseUrl}/{endpoint}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            // Determine if the response is a JSON object or array
            if (content.Trim().StartsWith("["))
            {
                // Handle JSON array
                return JArray.Parse(content);
            }
            else
            {
                // Handle JSON object
                return JObject.Parse(content);
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"General error: {e.Message}");
            return null;
        }
    }
    public async Task<JToken> PostDataToApi(string baseUrl, string endpoint, JObject postData)
    {
        try
        {
            // Serialize the JSON object to a string
            var content = new StringContent(postData.ToString(), Encoding.UTF8, "application/json");

            // Make the POST request
            var response = await _httpClient.PostAsync($"{baseUrl}/{endpoint}", content);
            response.EnsureSuccessStatusCode();

            // Read and parse the response
            var responseContent = await response.Content.ReadAsStringAsync();

            // Determine if the response is a JSON object or array
            if (responseContent.Trim().StartsWith("["))
            {
                // Handle JSON array
                return JArray.Parse(responseContent);
            }
            else
            {
                // Handle JSON object
                return JObject.Parse(responseContent);
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"General error: {e.Message}");
            return null;
        }
    }
}
