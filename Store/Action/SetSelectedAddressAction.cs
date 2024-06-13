using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class SetSelectedAddressAction
    {
        public AddressViewModel Address { get; }
        public SetSelectedAddressAction(AddressViewModel address) => Address = address;
    }
}
