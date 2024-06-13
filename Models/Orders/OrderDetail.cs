using System.ComponentModel.DataAnnotations;
using ApplicationToSellThings.BlazorUI.Models.Products;

namespace ApplicationToSellThings.BlazorUI.Models.Orders
{
    public class OrderDetail
    {
        [Key]
        public Guid OrderDetailId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public Product Product { get; set; }
    }
}
