using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Services.Interfaces
{
    public interface IAddressService
    {
        Task<AddressViewModel> AddNewAddress(AddressRequestModel addressRequestModel);
        Task<List<AddressViewModel>> GetAddressByUser(Guid userId);
    }
}
