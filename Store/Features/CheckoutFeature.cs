using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Store.State;
using Fluxor;

namespace ApplicationToSellThings.BlazorUI.Store.Features
{
    public class CheckoutFeature : Feature<CheckoutState>
    {
        public override string GetName() => "Checkout";

        protected override CheckoutState GetInitialState()
        {
            return new CheckoutState(
                selectedProducts: null,
                selectedAddress: null,
                addresses: new List<AddressViewModel>(),
                paymentOption: null,
                cardDetails: new List<CardDetail>(),
                selectedCard: null,
                isOrderReady: false,
                quantities: null,
                totalPrice: 0,
                isLoading: false,
                errorMessage: null
            );
        }
    }
}
