using System.Net.Http.Headers;
using Blazored.LocalStorage;

public class TokenService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public TokenService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task AttachTokenAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");

        if (!string.IsNullOrEmpty(token))
        {
            Console.WriteLine($"Attaching Token: {token}"); // Debugging
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            Console.WriteLine("No token found in LocalStorage");
        }
    }
}
