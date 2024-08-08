using Newtonsoft.Json.Linq;
using System.Text;


namespace ApiService
{
    public class ApiServices
    {
        private readonly HttpClient _httpClient;

        public ApiServices()
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
                var content = new StringContent(postData.ToString(), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{baseUrl}/{endpoint}", content);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                // Determine if the response is a JSON object or array
                if (responseContent.Trim().StartsWith("["))
                {
                    return JArray.Parse(responseContent);
                }
                else
                {
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

        public async Task<JToken> PutDataToApi(string baseUrl, string endpoint, JObject putData)
        {
            try
            {
                var content = new StringContent(putData.ToString(), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{baseUrl}/{endpoint}", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response: {errorContent}");
                    return null;
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(responseContent))
                {
                    return "No content returned from the server.";
                }

                if (responseContent.Trim().StartsWith("["))
                {
                    return JArray.Parse(responseContent);
                }
                else
                {
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

        public async Task<string> DeleteDataFromApi(string baseUrl, string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{baseUrl}/{endpoint}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    Console.WriteLine($"Request URI: {response.RequestMessage.RequestUri}");
                    Console.WriteLine($"Error response: {errorContent}");
                    return null;
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseContent))
                {
                    return "No content returned from the server.";
                }

                if (responseContent.Trim().StartsWith("["))
                {
                    return JArray.Parse(responseContent).ToString();
                }
                else
                {
                    return JObject.Parse(responseContent).ToString();
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
}
