namespace ApplicationToSellThings.BlazorUI.Models.Orders
{
    public class OrderRequestModel
    {
        public Guid UserId { get; set; }
        public List<OrderProductModel> Products { get; set; }
        public Guid? CardId { get; set; }
        public Guid ShippingAddressId { get; set; }
        public string PaymentMethod { get; set; }
        public int Quantity { get; set; }
    }
}
