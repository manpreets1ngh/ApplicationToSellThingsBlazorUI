using ApplicationToSellThing.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Services.Interfaces;
using System.Text.Json;

namespace ApplicationToSellThings.BlazorUI.Services
{
    public class CardService : ICardService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NotificationService _notificationService;

        public CardService(IHttpClientFactory httpClientFactory, NotificationService notificationService)
        {
            _httpClientFactory = httpClientFactory;
            _notificationService = notificationService;
        }

        public async Task<CardDetail> AddNewCardForUser(CardRequestModel cardRequestModel)
        {            
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = await client.PostAsJsonAsync("/api/Card", cardRequestModel);
            if (result.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var responseData = await result.Content.ReadFromJsonAsync<ResponseViewModel<CardDetail>>(options);
                if (responseData.Status == "Success" || responseData.StatusCode == 200)
                {
                    _notificationService.Notify(new NotificationModel
                    {
                        Message = responseData.Message,
                        Type = NotificationMessageType.Success
                    });

                    return responseData.Data;
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

        public async Task<List<CardDetail>> GetCardDetailsForUser(Guid userId)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = client.GetAsync($"/api/Card/{userId}");

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
                    var responseData = JsonSerializer.Deserialize<ResponseViewModel<CardDetail>>(content.Result, options);
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
