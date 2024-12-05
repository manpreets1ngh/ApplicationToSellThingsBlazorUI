using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models.Products;
using ApplicationToSellThings.BlazorUI.Services.Interfaces;
using ApplicationToSellThings.BlazorUI.Store.Action.CartActions;
using ApplicationToSellThings.BlazorUI.Store.State;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace ApplicationToSellThings.BlazorUI.Services
{
    public class CartService : ICartService
    {
        [Inject]
        public IState<CartState> _cartState { get; set; }

        private readonly IDispatcher _dispatcher;

        public CartService(IState<CartState> cartState, IDispatcher dispatcher)
        {
            _cartState = cartState;
            _dispatcher = dispatcher;
        }

        public async Task LoadCartAsync()
        {
            // Load cart items from a persistent store if necessary
            // For example, fetch from a server or local storage
            var cartItems = await FetchCartItemsFromServer();
            foreach (var item in cartItems)
            {
                _dispatcher.Dispatch(new AddToCartAction(item));
            }
        }

        private Task<List<CartItem>> FetchCartItemsFromServer()
        {
            // Simulate fetching from a server or other persistent store
            return Task.FromResult(new List<CartItem>());
        }
        
        public void AddToCart(Product product)
        {
            var existingItem = _cartState.Value.Items.FirstOrDefault(i => i.ProductId == product.ProductId.ToString());
            if (existingItem != null)
            {
                _dispatcher.Dispatch(new UpdateProductQuantityAction(Guid.Parse(existingItem.ProductId), existingItem.Quantity + 1));
            }
            else
            {
                _dispatcher.Dispatch(new AddToCartAction(new CartItem(
                    product.ProductId.ToString(),
                    product.ProductName,
                    product.BrandName,
                    product.Price,
                    1
                )));

            }
        }

        public void UpdateQuantity(string productId, int quantity)
        {
            _dispatcher.Dispatch(new UpdateProductQuantityAction(Guid.Parse(productId), quantity));
        }

        public void RemoveFromCart(string productId)
        {
            _dispatcher.Dispatch(new RemoveFromCartAction(Guid.Parse(productId)));
        }
    }
}