using ApplicationToSellThings.BlazorUI.Models.Products;

namespace ApplicationToSellThings.BlazorUI.Services.Interfaces;

public interface ICartService
{
    Task LoadCartAsync();
    void AddToCart(Product product);
    void UpdateQuantity(string productId, int quantity);
    void RemoveFromCart(string productId);
}