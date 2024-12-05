using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Pages.Components;
using Microsoft.AspNetCore.Components;
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

        public void Notify(NotificationModel notification)
        {
            _snackbar.Clear(); // Ensure no previous snackbars are lingering

            // Display the snackbar with custom styles
            _snackbar.Add(builder =>
            {
                builder.OpenComponent(0, typeof(CustomToastMessage));
                builder.AddAttribute(1, "Title", notification.Title);
                builder.AddAttribute(2, "Message", notification.Message);
                builder.AddAttribute(3, "Icon", notification.Icon);
                builder.AddAttribute(4, "Severity", ConvertToMudBlazorSeverity(notification.Type));
                builder.CloseComponent();
            }, ConvertToMudBlazorSeverity(notification.Type), config =>
            {
                config.SnackbarVariant = Variant.Text;  // Ensures that no additional styling is applied
                config.ShowCloseIcon = false; // Show the close icon
                config.VisibleStateDuration = 5000; // 5 seconds
                config.HideTransitionDuration = 0; // No hide transition
                config.ShowTransitionDuration = 0; // No show transition
            });
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
