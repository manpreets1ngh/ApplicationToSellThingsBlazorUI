using ApplicationToSellThings.BlazorUI.Models.Products;

namespace ApplicationToSellThings.BlazorUI.Models.Orders;

public class OrderDetailResponseViewModel
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public Product Product { get; set; }

}