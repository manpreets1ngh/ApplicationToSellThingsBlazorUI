using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Store.State;
public class CartState
{
    public List<CartItem> Items { get; }

    public CartState(List<CartItem> items)
    {
        Items = items;
    }

    // Property to return the initial state of the Cart
    public static CartState InitialCartState => new CartState(new List<CartItem>());

}