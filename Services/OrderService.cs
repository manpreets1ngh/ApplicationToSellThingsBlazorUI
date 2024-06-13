using ApplicationToSellThing.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models.Orders;
using ApplicationToSellThings.BlazorUI.Services.Interfaces;
using System.Text.Json;

namespace ApplicationToSellThings.BlazorUI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NotificationService _notificationService;

        public OrderService(IHttpClientFactory httpClientFactory, NotificationService notificationService)
        {
            _httpClientFactory = httpClientFactory;
            _notificationService = notificationService;
        }

        public async Task<OrderResponseViewModel> PlaceOrder(OrderRequestModel orderRequestModel)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = await client.PostAsJsonAsync("/api/Orders", orderRequestModel);
            if (result.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var responseData = await result.Content.ReadFromJsonAsync<ResponseViewModel<OrderResponseViewModel>>(options);
                if (responseData.Status == "Success" || responseData.StatusCode == 200)
                {
                    var orderResponse = new OrderResponseViewModel
                    {
                        PaymentMethod = responseData.Data.PaymentMethod,
                        OrderStatus = responseData.Data.OrderStatus,
                        TotalAmount = responseData.Data.TotalAmount,
                        Tax = responseData.Data.Tax,
                        Quantity = responseData.Data.Quantity,
                        Product = responseData.Data.Product,
                        OrderCreatedAt = responseData.Data.OrderCreatedAt                       
                    };

                    return orderResponse;
                }
                else
                {
                    _notificationService.Notify(new NotificationModel
                    {
                        Message = "Failed to add address: " + responseData.Message,
                        Type = NotificationMessageType.Error
                    });

                }

            }
            return null;
        }

        public async Task<List<Order>> GetOrdersListForUser(Guid userId)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = client.GetAsync($"/api/Orders/{userId}");

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
                    var responseData = JsonSerializer.Deserialize<ResponseViewModel<Order>>(content.Result, options);
                    if (responseData.Status == "Success" || responseData.StatusCode == 200)
                    {
                        return responseData.Items;
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
            else
            {
                return null;
            }

            return null;
        }
    }
}
