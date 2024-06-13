using ApplicationToSellThings.BlazorUI.Models.Products;

namespace ApplicationToSellThings.BlazorUI.Services.Interfaces
{
    public interface IProductsService
    {
        Task<List<Product>> GetProductsAsync();
        Task<ProductViewModel> GetProductByProductId(Guid productId);
    }
}
