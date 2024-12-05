using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Services.Interfaces
{
    public interface IResetForgotPasswordService
    {
        Task<ForgotPasswordModel> ForgotPasswordForuser(ForgotPasswordModel forgotPassModel);
        Task<ResetPasswordModel> ResetPasswordForuser(string email, string token, ResetPasswordModel resetPassModel);
    }
}
