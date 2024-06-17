using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Store.State;
public class CartState
{
    public List<CartItem> Items { get; }

    public CartState(List<CartItem> items)
    {
        Items = items;
    }

}