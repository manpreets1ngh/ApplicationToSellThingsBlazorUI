using ApplicationToSellThing.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models.Products;
using ApplicationToSellThings.BlazorUI.Services.Interfaces;
using System.Text.Json;

namespace ApplicationToSellThings.BlazorUI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NotificationService _notificationService;

        public ProductsService(IHttpClientFactory httpClientFactory, NotificationService notificationService)
        {
            _httpClientFactory = httpClientFactory;
            _notificationService = notificationService;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            List<Product> products = new List<Product>();
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var response = await client.GetAsync("/api/Products");

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var responseData = await response.Content.ReadFromJsonAsync<ResponseViewModel<Product>>(options);
                if (responseData.Status == "Success" || responseData.StatusCode == 200)
                {
                    products = responseData.Items.ToList();
                }
                else
                {
                    _notificationService.Notify(new NotificationModel
                    {
                        Message = "Failed to load products: " + responseData.Message,
                        Type = NotificationMessageType.Warning
                    });
                }
            }
            else
            {
                // Trigger error notification for unsuccessful HTTP response
                _notificationService.Notify(new NotificationModel
                {
                    Message = "API request failed with status code: " + response.StatusCode,
                    Type = NotificationMessageType.Error
                });
            }

            return products;
        }

        public async Task<ProductViewModel> GetProductByProductId(Guid productId)
        {
            ProductViewModel product = new ProductViewModel();
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var response = await client.GetAsync($"/api/Products/{productId}");

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var responseData = await response.Content.ReadFromJsonAsync<ResponseViewModel<Product>>(options);
                if (responseData.Status == "Success" || responseData.StatusCode == 200)
                {
                    product.ProductId = responseData.Data.ProductId;
                    product.ProductName = responseData.Data.ProductName;
                    product.BrandName = responseData.Data.BrandName;
                    product.Description = responseData.Data.Description;
                    product.Price = responseData.Data.Price;
                    product.Discount = responseData.Data.Discount;
                    product.Category = responseData.Data.Category;
                    product.QuantityInStock = responseData.Data.QuantityInStock;
                }
                else
                {
                    product = null;
                }
            }
            else
            {
                // Trigger error notification for unsuccessful HTTP response
                _notificationService.Notify(new NotificationModel
                {
                    Message = "API request failed with status code: " + response.StatusCode,
                    Type = NotificationMessageType.Error
                });
            }

            return product;
        }

    }
}
