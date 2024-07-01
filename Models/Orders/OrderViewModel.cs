namespace ApplicationToSellThings.BlazorUI.Models.Orders;

public class OrderViewModel
{
    public string OrderId { get; set; }
    public string UserId { get; set; }
    public string PaymentMethod { get; set; }
    public string CardId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Tax { get; set; }
    public DateTime? OrderCreatedAt { get; set; }
    public List<OrderDetailViewModel> OrderDetails { get; set; }
    public string OrderStatus { get; set; }
}