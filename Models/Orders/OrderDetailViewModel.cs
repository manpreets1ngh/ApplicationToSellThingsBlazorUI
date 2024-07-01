using ApplicationToSellThings.BlazorUI.Models.Products;

namespace ApplicationToSellThings.BlazorUI.Models.Orders;

public class OrderDetailViewModel
{
    public string ProductId { get; set; }
    public Product Product { get; set; }
    public decimal Total { get; set; }
    public int Quantity { get; set; }
}