using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Web_Labb2.BlazorServer.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private bool _isPrerendering = true;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // During prerendering, don't attempt to access local storage
            if (_isPrerendering)
            {
                return new AuthenticationState(_anonymous);
            }

            var token = await _localStorageService.GetItemAsync<string>("token");

            // If no token is found or it's invalid, return anonymous user state
            if (string.IsNullOrEmpty(token) || !IsValidJwtToken(token))
            {
                return new AuthenticationState(_anonymous);
            }

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void MarkAsRendered()
        {
            _isPrerendering = false;
        }

        public void NotifyUserAuthentication(string token)
        {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        private bool IsValidJwtToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                return jsonToken != null;
            }
            catch (Exception)
            {
                return false; // Invalid token format or expired token
            }
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;

            if (jsonToken == null)
                throw new ArgumentException("Invalid JWT token");

            claims.AddRange(jsonToken?.Claims);
            return claims;
        }
    }
}
