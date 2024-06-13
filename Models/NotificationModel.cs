namespace ApplicationToSellThings.BlazorUI.Models
{
    public class NotificationModel
    {
        public string Message { get; set; }
        public NotificationMessageType Type { get; set; }
    }

    public enum NotificationMessageType
    {
        Success,
        Error,
        Info,
        Warning
    }
}
