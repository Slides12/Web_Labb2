using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System.Text;
using Web_Labb2.BlazorServer.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// JWT Configuration from appsettings
var config = builder.Configuration;
var jwtKey = config["JwtSettings:Key"];
var issuer = config["JwtSettings:Issuer"];
var audience = config["JwtSettings:Audience"];

// Ensure jwtKey is not null or empty
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT Key is not configured.");
}

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = key
        };
    });

// Add Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy => policy.RequireAuthenticatedUser());
});

// Register HttpClient
builder.Services.AddScoped<HttpClient>(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri("https://localhost:7218/") };
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    return client;
});

// Register Blazored LocalStorage service
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthorizationHandler>();

var app = builder.Build();

// Middleware for Authentication & Authorization
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Ensure correct middleware order
app.UseAuthentication(); // Make sure authentication is before authorization
app.UseAuthorization();  // Authorization will work after authentication

// Add anti-forgery middleware to handle CSRF protection
app.UseAntiforgery();

// Optional: If you are using CORS, enable it if needed
// app.UseCors("AllowAllOrigins"); // Uncomment if needed

// Map Razor Components and Interactive Server Render Mode
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
