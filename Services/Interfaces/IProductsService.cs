using ApplicationToSellThings.BlazorUI.Models.Products;

namespace ApplicationToSellThings.BlazorUI.Services.Interfaces
{
    public interface IProductsService
    {
        Task<ProductViewModel> CreateProductAsync(Product product);
        Task<List<Product>> GetProductsAsync();
        Task<ProductViewModel> GetProductByProductId(Guid productId);
        Task<Product> UpdateProduct(Guid productId, Product product);
    }
}
