using ApplicationToSellThing.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Services.Interfaces;
using MudBlazor;
using System.Text.Json;

namespace ApplicationToSellThings.BlazorUI.Services
{
    public class ResetForgotPasswordService : IResetForgotPasswordService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NotificationService _notificationService;

        public ResetForgotPasswordService(IHttpClientFactory httpClientFactory, NotificationService notificationService)
        {
            _httpClientFactory = httpClientFactory;
            _notificationService = notificationService;
        }

        public async Task<ForgotPasswordModel> ForgotPasswordForuser(ForgotPasswordModel forgotPassModel)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = await client.PostAsJsonAsync("/api/Auth/forgot-password", forgotPassModel);
            if (result.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var responseData = await result.Content.ReadFromJsonAsync<ResponseViewModel<ForgotPasswordModel>>(options);
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
                        Message = responseData.Message,
                        Type = NotificationMessageType.Warning
                    });

                }

            }
            else
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var responseData = await result.Content.ReadFromJsonAsync<ResponseViewModel<ForgotPasswordModel>>(options);
                if (responseData.Status == "Warning" || responseData.StatusCode == 404)
                {
                    // Handle unexpected API errors
                    _notificationService.Notify(new NotificationModel
                    {
                        Icon = Icons.Material.Outlined.Warning,
                        Title = "Warning",
                        Message = responseData.Message,
                        Type = NotificationMessageType.Warning,
                        Duration = 5000
                    });

                    return responseData.Data;
                }
                
            }

            return null;

        }

        public async Task<ResetPasswordModel> ResetPasswordForuser(string email, string token, ResetPasswordModel resetPassModel)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var endpoint = $"/api/Auth/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";
            var result = await client.PostAsJsonAsync(endpoint, resetPassModel);
            if (result.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var responseData = await result.Content.ReadFromJsonAsync<ResponseViewModel<ResetPasswordModel>>(options);
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
    }
}
