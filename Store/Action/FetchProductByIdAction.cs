namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class FetchProductByIdAction
    {
        public Guid ProductId { get; }
        public FetchProductByIdAction(Guid productId) => ProductId = productId;
    }
}
