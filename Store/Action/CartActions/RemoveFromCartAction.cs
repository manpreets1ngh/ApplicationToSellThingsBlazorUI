namespace ApplicationToSellThings.BlazorUI.Store.Action.CartActions
{
    public class RemoveFromCartAction
    {
        public Guid ProductId { get; }

        public RemoveFromCartAction(Guid productId) => ProductId = productId;
    }
}