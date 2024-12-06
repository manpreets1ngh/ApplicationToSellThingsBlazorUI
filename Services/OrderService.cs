using ApplicationToSellThing.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models.Orders;
using ApplicationToSellThings.BlazorUI.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ApplicationToSellThings.BlazorUI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NotificationService _notificationService;
        private readonly IAuthService _authService;
        public OrderService(IHttpClientFactory httpClientFactory, NotificationService notificationService, IAuthService authService)
        {
            _httpClientFactory = httpClientFactory;
            _notificationService = notificationService;
            _authService = authService;
        }

        public async Task<OrderResponseViewModel> PlaceOrder(OrderRequestModel orderRequestModel, string token)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostAsJsonAsync("/api/Orders", orderRequestModel);
            if (result.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var responseData = await result.Content.ReadFromJsonAsync<ResponseViewModel<OrderResponseViewModel>>(options);
                if (responseData.Status == "Success" || responseData.StatusCode == 200)
                {
                    return new OrderResponseViewModel
                    {
                        OrderNumber = responseData.Data.OrderNumber,
                        PaymentMethod = responseData.Data.PaymentMethod,
                        OrderStatus = responseData.Data.OrderStatus,
                        TotalAmount = responseData.Data.TotalAmount,
                        Tax = responseData.Data.Tax,
                        OrderCreatedAt = responseData.Data.OrderCreatedAt,
                        ShippingDetails = new ShippingInfoModel
                        {
                            ShippingInfoId = responseData.Data.ShippingDetails.ShippingInfoId,
                            DeliveryStatusId = responseData.Data.ShippingDetails.DeliveryStatusId,
                            EstimatedDeliveryDate = responseData.Data.ShippingDetails.EstimatedDeliveryDate,
                            DeliveryStatus = new StatusModel
                            {
                                Id = responseData.Data.ShippingDetails.DeliveryStatusId,
                                Name = responseData.Data.ShippingDetails.DeliveryStatus.Name,
                                Alias = responseData.Data.ShippingDetails.DeliveryStatus.Alias,
                                Type = responseData.Data.ShippingDetails.DeliveryStatus.Type
                            }
                        },
                        OrderDetails = responseData.Data.OrderDetails.Select(od => new OrderDetailResponseViewModel
                        {
                            ProductId = od.ProductId,
                            ProductName = od.ProductName,
                            Quantity = od.Quantity,
                            Total = od.Total
                        }).ToList()
                    };
                }
                else
                {
                    _notificationService.Notify(new NotificationModel
                    {
                        Message = "Failed to place order: " + responseData.Message,
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
            return null;
        }

        public async Task<Order> GetOrderByOrderNumber(string orderNumber)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = client.GetAsync($"/api/Orders/orderNumber/{orderNumber}");

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

        public async Task<List<Order>> GetOrders()
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = client.GetAsync($"/api/Orders");

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
        
        public async Task<Order> UpdateOrder(Guid orderId, Order order)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = client.PutAsJsonAsync<Order>($"/api/Orders/{orderId}", order);

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


        public async Task<ShippingInfoModel> UpdateShippingInfo(Guid orderId, ShippingInfoModel shippingInfoModel)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = client.PutAsJsonAsync($"/api/Orders/{orderId}/shipping-info", shippingInfoModel);

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
                    var responseData = JsonSerializer.Deserialize<ResponseViewModel<ShippingInfoModel>>(content.Result, options);
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

        public async Task<ShippingInfoModel> GetShippingInfo(Guid orderId)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = client.GetAsync($"/api/Orders/{orderId}/shipping-info");

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
                    var responseData = JsonSerializer.Deserialize<ResponseViewModel<ShippingInfoModel>>(content.Result, options);
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

        public async Task<Order> UpdateOrderStatusandShippingInfo(Guid orderId, string newStatus, ShippingInfoModel? shippingInfo = null)
        {
            var payload = new
            {
                OrderStatus = newStatus,
                ShippingInfo = shippingInfo
            };

            try
            { 
                var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
                var result = client.PutAsJsonAsync($"/api/Orders/{orderId}/status-and-shipping-info", payload);

                if (result.Result.IsSuccessStatusCode)
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
                        return responseData.Data;
                    }
                }
            }
            catch (Exception ex)
            {
                // Notify the user about an exception
                _notificationService.Notify(new NotificationModel
                {
                    Message = $"Error while updating order: {ex.Message}",
                    Type = NotificationMessageType.Error
                });
            }

            return null;
        }
    }
}
