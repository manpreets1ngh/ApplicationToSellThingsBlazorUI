using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Store.Action.CartActions;
using ApplicationToSellThings.BlazorUI.Store.State;
using Fluxor;

namespace ApplicationToSellThings.BlazorUI.Store.Reducer;

public class CartReducer
{
    [ReducerMethod]
    public static CartState ReduceAddToCartAction(CartState state, AddToCartAction action)
    {
        var existingItem = state.Items.FirstOrDefault(i => i.ProductId == action.Item.ProductId);
        if (existingItem != null)
        {
            var updatedItems = state.Items.Select(i =>
                i.ProductId == action.Item.ProductId 
                    ? new CartItem(i.ProductId, i.ProductName, i.BrandName, i.Price, i.Quantity + action.Item.Quantity) 
                    : i).ToList();
            return new CartState(updatedItems);
        }

        var newItems = state.Items.ToList();
        newItems.Add(action.Item);
        return new CartState(newItems);
    }

    [ReducerMethod]
    public static CartState ReduceRemoveFromCartAction(CartState state, RemoveFromCartAction action)
    {
        var updatedItems = state.Items.Where(i => i.ProductId != action.ProductId.ToString()).ToList();
        return new CartState(updatedItems);
    }

    [ReducerMethod]
    public static CartState ReduceUpdateProductQuantityAction(CartState state, UpdateProductQuantityAction action)
    {
        var updatedItems = state.Items.Select(i =>
            i.ProductId == action.ProductId.ToString() 
                ? new CartItem(i.ProductId, i.ProductName, i.BrandName, i.Price, action.Quantity) 
                : i).ToList();
        return new CartState(updatedItems);
    }

    [ReducerMethod]
    public static CartState ReduceClearCartAction(CartState state, ClearCartAction action)
    {
        return new CartState(new List<CartItem>());

    }
}