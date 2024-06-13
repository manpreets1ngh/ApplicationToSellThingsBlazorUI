using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class AddNewAddressAction
    {
        public AddressRequestModel Address { get; }
        public AddNewAddressAction(AddressRequestModel address) => Address = address;
    }
}
