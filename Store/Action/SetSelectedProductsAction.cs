using ApplicationToSellThings.BlazorUI.Models.Products;

namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class SetSelectedProductsAction
    {
        public List<ProductViewModel> Products { get; }
        public SetSelectedProductsAction(List<ProductViewModel> products) => Products = products;
    }
}
