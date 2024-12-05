using ApplicationToSellThing.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Services.Interfaces;
using ApplicationToSellThings.BlazorUI.Store.Action;
using Fluxor;
using Microsoft.AspNetCore.Components;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace ApplicationToSellThings.BlazorUI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NavigationManager _navigationManager;
        public IHttpContextAccessor _httpContextAccessor;
        private readonly IDispatcher _dispatcher;
        private bool _isAuthenticated;
        public AuthService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager, IHttpContextAccessor httpContextAccessor, IDispatcher dispatcher)
        {
            _httpClientFactory = httpClientFactory;
            _navigationManager = navigationManager;
            _httpContextAccessor = httpContextAccessor;
            _dispatcher = dispatcher;
        }

        public bool IsAuthenticated => _isAuthenticated;

        public async Task<ResponseViewModel<string>> UserLogin(LoginUser loginUser, string returnUrl = null)
        {
            List<string> userRoles = new List<string>();
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = await client.PostAsJsonAsync("/api/Auth/login", loginUser);

            if (result.IsSuccessStatusCode)
            {
                _isAuthenticated = true;
                var token = await result.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultData = JsonSerializer.Deserialize<ResponseViewModel<string>>(token, options);

                if (resultData.Status == "Error")
                {
                    // Consider using a notification service or another method to communicate this back to the UI
                    return new ResponseViewModel<string>()
                    {
                        Status= resultData.Status,
                        StatusCode = resultData.StatusCode,
                        Data= resultData.Data,
                        Items = resultData.Items,
                        Message= resultData.Message
                    };;
                }
                
                var tokenValue = resultData.Data;
                userRoles = resultData.Items;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(tokenValue) as JwtSecurityToken;
                var userId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "jti")?.Value;
                var userDetail = await GetUserDetailsByIdAsync(userId);

                _dispatcher.Dispatch(new LoginAction(tokenValue, userDetail.Data.UserName, userDetail.Data.Email, userRoles));
                // Redirect logic based on roles
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    _navigationManager.NavigateTo(returnUrl);
                }
                else
                {
                    // Update this as per your role-based redirection logic
                    _navigationManager.NavigateTo(userRoles.Contains("Admin") ? "/admin/dashboard" : "/user/dashboard");
                }

                return new ResponseViewModel<string>()
                {
                    Status= resultData.Status,
                    StatusCode = resultData.StatusCode,
                    Data= resultData.Data,
                    Items = resultData.Items,
                    Message= resultData.Message
                };
            }
            else
            {
                // Handle non-success status codes (e.g., 401, 404)
                var errorContent = await result.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var errorResult = JsonSerializer.Deserialize<ResponseViewModel<string>>(errorContent, options);

                if (result.StatusCode == System.Net.HttpStatusCode.NotFound && errorResult?.Message.Contains("The email you entered does not match any account. Please check your email and try again.") == true)
                {
                    // Return with a toast notification indicator
                    return new ResponseViewModel<string>()
                    {
                        Status = "Error",
                        StatusCode = (int)result.StatusCode,
                        Message = errorResult.Message,
                        Data = null
                    };
                }

                return new ResponseViewModel<string>()
                {
                    Status = "Error",
                    StatusCode = (int)result.StatusCode,    
                    Message = errorResult?.Message ?? "Internal Server Error. Please try again later.",
                    Data = null
                };
            }
        }
        
        public async Task<ResponseViewModel<string>> UserRegisterAccount(RegisterUser registerUser, string returnUrl = null)
        {
            List<string> userRoles = new List<string>();
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = await client.PostAsJsonAsync("/api/Auth/register", registerUser);

            if (result.IsSuccessStatusCode)
            {
                _isAuthenticated = true;
                var token = await result.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultData = JsonSerializer.Deserialize<ResponseViewModel<string>>(token, options);

                if (resultData.Status == "Error")
                {
                    // Consider using a notification service or another method to communicate this back to the UI
                    return new ResponseViewModel<string>()
                    {
                        Status= resultData.Status,
                        StatusCode = resultData.StatusCode,
                        Data= resultData.Data,
                        Items = resultData.Items,
                        Message= resultData.Message
                    };;
                }
                else
                {
                    return new ResponseViewModel<string>()
                    {
                        Status= resultData.Status,
                        StatusCode = resultData.StatusCode,
                        Data= resultData.Data,
                        Items = resultData.Items,
                        Message= resultData.Message
                    };
                }
            }
            else
            {
                return new ResponseViewModel<string>() {
                    Message = "API request not proceeeded...",
                    Status = "Internal Server Error",
                    StatusCode = 500
                };
            }
        }

        public async Task<ResponseViewModel<UserDetail>> GetUserDetailsByIdAsync(string userId)
        {
            var client = _httpClientFactory.CreateClient("ApplicationToSellthingsAPI");
            var result = await client.GetAsync($"/api/Auth/{userId}");
            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultData = JsonSerializer.Deserialize<ResponseViewModel<UserDetail>>(data, options);

                return new ResponseViewModel<UserDetail>
                {
                    Status = resultData.Status,
                    StatusCode = resultData.StatusCode,
                    Data= resultData.Data,
                };
            }
            return null;
        }

        public void SetAuthenticated(bool isAuthenticated)
        {
            _isAuthenticated = isAuthenticated;
        }


        public async Task SignOutAsync()
        {
            _dispatcher.Dispatch(new LogoutAction());
            _isAuthenticated = false;
        }
    }
}
