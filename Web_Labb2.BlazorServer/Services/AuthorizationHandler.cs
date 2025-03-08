using Blazored.LocalStorage;

public class AuthorizationHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public AuthorizationHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Hämta token från LocalStorage
        var token = await _localStorage.GetItemAsync<string>("authToken");

        // Lägg till token i Authorization-header om den finns
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        // Skicka begäran
        return await base.SendAsync(request, cancellationToken);
    }
}
