using ApplicationToSellThing.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Services.Interfaces;
using System.Text.Json;

namespace ApplicationToSellThings.BlazorUI.Services
{
    public class AddressService : IAddressService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NotificationService _notificationService;

        public AddressService(IHttpClientFactory httpClientFactory, NotificationService notificationService)
        {
            _httpClientFactory = httpClientFactory;
            _notificationService = notificationService;
        }

        public async Task<AddressViewModel> AddNewAddress(AddressRequestModel addressRequestModel)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = await client.PostAsJsonAsync("/api/Address", addressRequestModel);
            if (result.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var responseData = await result.Content.ReadFromJsonAsync<ResponseViewModel<AddressViewModel>>(options);
                if (responseData.Status == "Success" || responseData.StatusCode == 200)
                {
                    var addressResponse = new AddressViewModel
                    {
                        Id = responseData.Data.Id,
                        UserId = responseData.Data.UserId,
                        Street = responseData.Data.Street,
                        City = responseData.Data.City,
                        State = responseData.Data.State,
                        PostCode = responseData.Data.PostCode,
                        Country = responseData.Data.Country,
                    };

                    _notificationService.Notify(new NotificationModel
                    {
                        Message = responseData.Message,
                        Type = NotificationMessageType.Success
                    });

                    return addressResponse;
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

        public async Task<List<AddressViewModel>> GetAddressByUser(Guid userId)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = client.GetAsync($"/api/Address/{userId}");

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
                    var responseData = JsonSerializer.Deserialize<ResponseViewModel<AddressViewModel>>(content.Result, options);
                    if (responseData.Status == "Success" || responseData.StatusCode == 200)
                    {
                        return responseData.Items;
                    }
                }
                catch(Exception ex)
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
