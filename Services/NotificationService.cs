using ApplicationToSellThings.BlazorUI.Models;
using Blazored.Toast.Services;

namespace ApplicationToSellThings.BlazorUI.Services
{
    public class NotificationService
    {
        private readonly IToastService _toastService;

        public NotificationService(IToastService toastService)
        {
            _toastService = toastService;
        }

        public void Notify(NotificationModel message)
        {
            if (message.Type == NotificationMessageType.Success)
            {
                _toastService.ShowSuccess(message.Message);
            }

            else if (message.Type == NotificationMessageType.Error)
            {
                _toastService.ShowError(message.Message);
            }
        }
    }
}
