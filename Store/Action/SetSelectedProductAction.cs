using ApplicationToSellThings.BlazorUI.Models.Products;

namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class SetSelectedProductAction
    {
        public ProductViewModel Product { get; }
        public SetSelectedProductAction(ProductViewModel product) => Product = product;
    }
}
