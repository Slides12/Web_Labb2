using Microsoft.AspNetCore.Authentication;
using System.Text.Json;
using Web_Labb2.Shared.DTO_s;

namespace Web_Labb2.BlazorServer.Services
{
    public class AuthService
    {

        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponse?> Login(LoginDTO request)
        {
            var result = await _httpClient.PostAsJsonAsync("https://localhost:7218/api/Auth/login", request);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                Console.WriteLine("API Response: " + content); // Add this line to check the raw response

                // Deserialize the JSON response
                var authResponse = JsonSerializer.Deserialize<AuthResponse>(content);

                if (authResponse != null)
                {
                    Console.WriteLine("Deserialized Response: " + authResponse.Token); // Check the deserialized response
                }

                return authResponse;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Register (UserDTO request)
        {
            var result = await _httpClient.PostAsJsonAsync("https://localhost:7218/api/Auth/register", request);
            return result.IsSuccessStatusCode;
        }
    }

}
