using ApplicationToSellThings.BlazorUI.Models.Orders;
using ApplicationToSellThings.BlazorUI.Models.Products;

namespace ApplicationToSellThings.BlazorUI.Models.AdminPanel;

public class DashboardMetrics
{
    public decimal TotalSales { get; set; }
    public int TotalOrders { get; set; }
    public int TotalCustomers { get; set; }
    public decimal TotalRevenue { get; set; }
    public List<ChartSeries> SalesTrends { get; set; }
    public int TotalProducts { get; set; }
    
    public List<Order> RecentOrders { get; set; }
    public List<UserDetail> RecentCustomers { get; set; }
}