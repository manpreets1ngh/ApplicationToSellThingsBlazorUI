namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class UpdateProductQuantityAction
    {
        public int Quantity { get; }
        public UpdateProductQuantityAction(int quantity) => Quantity = quantity;
    }
}
