using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Services.Interfaces
{
    public interface IStatusService
    {
        Task<StatusModel> GetStatusInfoById(int statusId);
        Task<List<StatusModel>> GetAllStatusInfo();
    }
}
