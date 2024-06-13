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
                selectedProduct: null,
                selectedAddress: null,
                addresses: new List<AddressViewModel>(),
                paymentOption: null,
                cardDetails: new List<CardDetail>(),
                selectedCard: null,
                isOrderReady: false,
                quantity: 0,
                totalPrice: 0,
                isLoading: false,
                errorMessage: null
            );
        }
    }
}
