using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class SetAddressesAction
    {
        public IEnumerable<AddressViewModel> Addresses { get; }

        public SetAddressesAction(IEnumerable<AddressViewModel> addresses)
        {
            Addresses = addresses;
        }
    }
}
