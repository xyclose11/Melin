using System.Net.Http;
using System.Threading.Tasks;

namespace Melin.Server;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDataFromApiAsync(string apiUrl)
    {
        // Make the GET request
        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

        // Ensure the response is successful
        response.EnsureSuccessStatusCode();

        // Read the content
        string content = await response.Content.ReadAsStringAsync();
        return content;
    }
}
