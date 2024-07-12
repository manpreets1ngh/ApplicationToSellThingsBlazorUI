using ApplicationToSellThing.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models.Orders;
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

        public async Task<ProductViewModel> CreateProductAsync(Product product)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = await client.PostAsJsonAsync($"/api/Products", product);
            if (result.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var responseData = await result.Content.ReadFromJsonAsync<Product>(options);
                if (responseData != null)
                {
                    var productResponse = new ProductViewModel
                    {
                        ProductId = responseData.ProductId,
                        ProductName = responseData.ProductName,
                        BrandName = responseData.BrandName,
                        Description = responseData.Description,
                        Price = responseData.Price,
                        Discount = responseData.Discount,
                        Category = responseData.Category,
                        QuantityInStock = responseData.QuantityInStock,
                        CreatedAt = responseData.CreatedAt,
                        ProductImage = responseData.ProductImage,
                    };

                    _notificationService.Notify(new NotificationModel
                    {
                        Message = "Product Created Successfully",
                        Type = NotificationMessageType.Success
                    });

                    return productResponse;
                }
                else
                {
                    _notificationService.Notify(new NotificationModel
                    {
                        Message = "Failed to create product" ,
                        Type = NotificationMessageType.Error
                    });

                }
            }
            return null;
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

        public async Task<Product> UpdateProduct(Guid productId, Product product)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = client.PutAsJsonAsync($"/api/Products/{productId}", product);
            if (result.Result.IsSuccessStatusCode)
            {
                try
                {
                    // Deserialize the response object into list of Products
                    var content = result.Result.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var responseData = JsonSerializer.Deserialize<ResponseViewModel<Product>>(content.Result, options);
                    if (responseData.Status == "Success" || responseData.StatusCode == 200)
                    {
                        return responseData.Data;
                    }
                }
                catch (Exception ex)
                {
                    _notificationService.Notify(new NotificationModel
                    {
                        Message = ex.Message,
                        Type = NotificationMessageType.Error
                    });
                }
            }
            return null;

        }
    }
}
