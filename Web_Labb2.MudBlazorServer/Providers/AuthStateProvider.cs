using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Web_Labb2.MudBlazorServer.Providers
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public AuthStateProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("jwt_token");
            var identity = new ClaimsIdentity();

            if(!string.IsNullOrEmpty(token))
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _localStorage.SetItemAsync("jwt_token", token);
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), token);
            var user = new ClaimsPrincipal(identity);

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("jwt_token");
            _http.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }


        private IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var payload = token.Split(".")[1];
            var jsonBytes = Convert.FromBase64String(PadBase64(payload));
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }


        private static string PadBase64(string base64)
        {
            while(base64.Length % 4 != 0)
            {
                base64 += "=";
            }
            return base64;
        }
    }
}
