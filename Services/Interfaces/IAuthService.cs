using ApplicationToSellThing.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseViewModel<string>> UserLogin(LoginUser loginUser, string returnUrl = null);
        Task<ResponseViewModel<UserDetail>> GetUserDetailsByIdAsync(string userId);
        Task SignOutAsync();
        bool IsAuthenticated { get; }

    }
}
