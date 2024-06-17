using ApplicationToSellThings.BlazorUI.Models.Products;

namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class SetProductsAction
    {
        public List<ProductViewModel> Products { get; }
        public SetProductsAction(List<ProductViewModel> products) => Products = products;
    }
}
