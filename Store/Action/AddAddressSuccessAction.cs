using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class AddAddressSuccessAction
    {
        public AddressViewModel Address { get; }
        public AddAddressSuccessAction(AddressViewModel address) => Address = address;
    }
}
