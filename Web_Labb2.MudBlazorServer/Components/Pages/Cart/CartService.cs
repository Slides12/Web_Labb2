using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;
using Web_Labb2.DTO_s;

public class CartService
{
    private readonly ProtectedLocalStorage _localStorage;
    public List<ProductDTO> CartItems { get; private set; } = new List<ProductDTO>();

    public event Action OnCartChanged;

    public CartService(ProtectedLocalStorage localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task LoadCart()
    {
        var storedCart = await _localStorage.GetAsync<string>("cart");
        if (storedCart.Success && !string.IsNullOrEmpty(storedCart.Value))
        {
            CartItems = JsonSerializer.Deserialize<List<ProductDTO>>(storedCart.Value) ?? new List<ProductDTO>();
        }
    }

    public async Task AddToCart(ProductDTO product)
    {
        CartItems.Add(product);
        await _localStorage.SetAsync("cart", JsonSerializer.Serialize(CartItems));
        OnCartChanged?.Invoke();
    }

    public async Task ClearCart()
    {
        CartItems.Clear();
        await _localStorage.DeleteAsync("cart");
        OnCartChanged?.Invoke();
    }

    public async Task RemoveItemFromCart(string id)
    {
        var removeProduct = CartItems.FirstOrDefault(p => p.ProductId == id);

        if(removeProduct != null)
        {
            CartItems.Remove(removeProduct);

            await _localStorage.SetAsync("cart", JsonSerializer.Serialize(CartItems));
            OnCartChanged?.Invoke();
        }
    }
}
