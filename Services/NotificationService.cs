using ApplicationToSellThings.BlazorUI.Models;
using MudBlazor;

namespace ApplicationToSellThings.BlazorUI.Services
{
    public class NotificationService
    {
        private readonly ISnackbar _snackbar;
        public NotificationService(ISnackbar snackbar)
        {
            _snackbar = snackbar;
        }

        public void Notify(NotificationModel message)
        {
            _snackbar.Add(message.Message, severity: ConvertToMudBlazorSeverity(message.Type));
        }

        private MudBlazor.Severity ConvertToMudBlazorSeverity(NotificationMessageType type)
        {
            return type switch
            {
                NotificationMessageType.Success => MudBlazor.Severity.Success,
                NotificationMessageType.Error => MudBlazor.Severity.Error,
                NotificationMessageType.Info => MudBlazor.Severity.Info,
                NotificationMessageType.Warning => MudBlazor.Severity.Warning,
                _ => MudBlazor.Severity.Info
            };
        }
    }
}
