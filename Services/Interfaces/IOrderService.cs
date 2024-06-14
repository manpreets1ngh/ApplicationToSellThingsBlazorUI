using ApplicationToSellThings.BlazorUI.Models.Orders;

namespace ApplicationToSellThings.BlazorUI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseViewModel> PlaceOrder(OrderRequestModel orderRequestModel, string token);
        Task<List<Order>> GetOrdersListForUser(Guid userId);
    }
}
