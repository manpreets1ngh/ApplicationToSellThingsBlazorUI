namespace ApplicationToSellThings.BlazorUI.Store.Action.CartActions
{
    public class UpdateProductQuantityAction
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public UpdateProductQuantityAction(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
