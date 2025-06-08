public class ExternalApiService
{
    private readonly HttpClient _httpClient;

    public ExternalApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDataFromApiAsync()
    {
        Console.WriteLine("Calling external API...");
        var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}