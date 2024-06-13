namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class FetchProductAction
    {
        public Guid ProductId { get; }
        public FetchProductAction(Guid productId) => ProductId = productId;
    }
}
