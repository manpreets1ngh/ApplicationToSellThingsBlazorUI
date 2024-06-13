using ApplicationToSellThings.BlazorUI.Models.Products;

namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class SetProductAction
    {
        public ProductViewModel Product { get; }
        public SetProductAction(ProductViewModel product) => Product = product;
    }
}
