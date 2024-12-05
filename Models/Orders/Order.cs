using System.ComponentModel.DataAnnotations;

namespace ApplicationToSellThings.BlazorUI.Models.Orders
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string OrderNumber { get; set; }
        public string PaymentMethod { get; set; }
        public Guid? CardId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Tax { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderCreatedAt { get; set; }
        public DateTime? OrderUpdatedAt { get; set; }
        public Guid ShippingInfoId { get; set; }
        public ShippingInfoModel ShippingInfo { get; set; }

        public Guid ShippingAddressId { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
