namespace ApplicationToSellThings.BlazorUI.Models
{
    public class NotificationModel
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public NotificationMessageType Type { get; set; }
        public int Duration { get; set; } = 5000;
    }

    public enum NotificationMessageType
    {
        Success,
        Error,
        Info,
        Warning
    }
}
