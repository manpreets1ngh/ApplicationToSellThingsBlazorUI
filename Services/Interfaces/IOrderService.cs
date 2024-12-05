using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models.Orders;

namespace ApplicationToSellThings.BlazorUI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseViewModel> PlaceOrder(OrderRequestModel orderRequestModel, string token);
        Task<List<Order>> GetOrdersListForUser(Guid userId);
        Task<Order> GetOrderByOrderNumber(string orderNumber);
        Task<List<Order>> GetOrders();
        Task<Order> UpdateOrder(Guid orderId, Order order);
        Task<ShippingInfoModel> UpdateShippingInfo(Guid orderId, ShippingInfoModel shippingInfoModel);
        Task<ShippingInfoModel> GetShippingInfo(Guid orderId);
        Task<Order> UpdateOrderStatusandShippingInfo(Guid orderId, string newStatus, ShippingInfoModel? shippingInfo = null);
    }
}
