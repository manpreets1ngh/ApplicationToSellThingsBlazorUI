using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Store.State;

namespace ApplicationToSellThings.BlazorUI.Store.Action.CartActions
{
    public class AddToCartAction
    {
        public CartItem Item { get; }

        public AddToCartAction(CartItem item) => Item = item;
    }
}