namespace ApplicationToSellThings.BlazorUI.Models;

public class CartItem
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string BrandName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public CartItem(string productId, string productName, string brandName, decimal price, int quantity)
    {
        ProductId = productId;
        ProductName = productName;
        BrandName = brandName;
        Price = price;
        Quantity = quantity;
    }

    // Copy constructor for creating new instances with modified properties
    public CartItem(CartItem item)
    {
        ProductId = item.ProductId;
        ProductName = item.ProductName;
        BrandName = item.BrandName;
        Price = item.Price;
        Quantity = item.Quantity;
    }
}