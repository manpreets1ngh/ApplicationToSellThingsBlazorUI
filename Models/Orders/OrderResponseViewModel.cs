﻿using ApplicationToSellThings.BlazorUI.Models.Products;

namespace ApplicationToSellThings.BlazorUI.Models.Orders
{
    public class OrderResponseViewModel
    {
        public string PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Tax { get; set; }
        public string OrderStatus { get; set; }
        public int Quantity { get; set; }
        public List<OrderDetailResponseViewModel> OrderDetails { get; set; }
        public DateTime? OrderCreatedAt { get; set; }
    }
}
