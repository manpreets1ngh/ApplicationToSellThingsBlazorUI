namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class FetchProductsAction
    {
        public List<Guid> ProductIds { get; }
        public FetchProductsAction(List<Guid> productIds) => ProductIds = productIds;
    }
}
