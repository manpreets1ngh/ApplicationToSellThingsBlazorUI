using ApplicationToSellThings.BlazorUI.Models.AdminPanel;
using ApplicationToSellThings.BlazorUI.Models.Orders;

namespace ApplicationToSellThings.BlazorUI.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardMetrics> GetDashboardMetricsAsync(DateTime? startDate = null, DateTime? endDate = null);
    Task<List<Order>> GetRecentOrdersAsync(int count = 5);
}