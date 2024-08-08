using BulkyWeb.Models;

public class CategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Category>>("api/Categories");
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Category>($"api/Categories/{id}");
    }

    public async Task CreateCategoryAsync(Category category)
    {
        await _httpClient.PostAsJsonAsync("api/Categories", category);
    }

    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Categories/{category.Id}", category);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Categories/{id}");
        return response.IsSuccessStatusCode;
    }
}
