using ApplicationToSellThings.BlazorUI.Models.AdminPanel;
using ApplicationToSellThings.BlazorUI.Models.Orders;
using ApplicationToSellThings.BlazorUI.Services.Interfaces;

namespace ApplicationToSellThings.BlazorUI.Services;

public class DashboardService : IDashboardService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly NotificationService _notificationService;
    private readonly IOrderService _orderService;
    private readonly IProductsService _productService;
    
    public DashboardService(IHttpClientFactory httpClientFactory, NotificationService notificationService, 
        IOrderService orderService, IProductsService productService)
    {
        _httpClientFactory = httpClientFactory;
        _notificationService = notificationService;
        _orderService = orderService;
        _productService = productService;
    }
    public async Task<DashboardMetrics> GetDashboardMetricsAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        if (!startDate.HasValue)
        {
            startDate = new DateTime(DateTime.Now.Year, 1, 1);
        }
        if (!endDate.HasValue)
        {
            endDate = startDate.Value.AddYears(1).AddDays(-1);
        }
        
        var ordersList = await _orderService.GetOrders();
        var orders = ordersList.Where(o => o.OrderCreatedAt >= startDate && o.OrderCreatedAt <= endDate)
            .ToList();
        var products = await _productService.GetProductsAsync();
        var totalSales = orders.Sum(o => o.TotalAmount);
        var totalOrders = orders.Count();
        var totalCustomers = 100;
        var totalRevenue = totalSales; // Assuming total sales and revenue are the same
        
        var salesTrends = orders
            .GroupBy(o => o.OrderCreatedAt.Value.Month)
            .Select(g => new ChartSeries 
            { 
                Name = g.Key.ToString(), 
                Data = g.Select(o => (double)o.TotalAmount).ToArray() 
            })
            .ToList();

        var totalProducts = products.Count();

        return new DashboardMetrics 
        {
            TotalSales = totalSales,
            TotalOrders = totalOrders,
            TotalCustomers = totalCustomers,
            TotalRevenue = totalRevenue,
            SalesTrends = salesTrends,
            TotalProducts = totalProducts
        };
    }
    
    public async Task<List<Order>> GetRecentOrdersAsync(int count = 5)
    {
        var ordersList = await _orderService.GetOrders();
        var orders =  ordersList
            .Where(o => o.OrderStatus == "Pending")
            .OrderByDescending(o => o.OrderCreatedAt)
            .Take(count)
            .ToList();

        return orders;
    }
}