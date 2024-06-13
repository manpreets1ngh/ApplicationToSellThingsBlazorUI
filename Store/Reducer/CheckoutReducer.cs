using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models.Products;
using ApplicationToSellThings.BlazorUI.Store.Action;
using ApplicationToSellThings.BlazorUI.Store.State;
using Fluxor;

namespace ApplicationToSellThings.BlazorUI.Store.Reducer
{
    public static class CheckoutReducers
    {
        [ReducerMethod]
        public static CheckoutState ReduceSetSelectedProductAction(CheckoutState state, SetSelectedProductAction action)
        {
            int quantityAvailable = 0; // Default quantity

            // Add logic here to calculate the quantity based on the selected product
            if (action.Product != null)
            {
                // For example, if the product has a property named "AvailableQuantity"
                quantityAvailable = action.Product.QuantityInStock;
            }

            return new CheckoutState(
                selectedProduct: action.Product,
                selectedAddress: state.SelectedAddress,
                addresses: state.Addresses,
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: CheckoutState.EvaluateOrderReadiness(state.SelectedAddress, state.PaymentOption, action.Product, state.SelectedCard),
                quantity: quantityAvailable,
                totalPrice: state.TotalPrice,
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceSetSelectedAddressAction(CheckoutState state, SetSelectedAddressAction action)
        {
            return new CheckoutState(
                selectedProduct: state.SelectedProduct,
                selectedAddress: action.Address,
                addresses: state.Addresses,
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: CheckoutState.EvaluateOrderReadiness(action.Address, state.PaymentOption, state.SelectedProduct, state.SelectedCard),
                quantity: state.Quantity,
                totalPrice: state.TotalPrice,
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceAddAddressSuccessAction(CheckoutState state, AddAddressSuccessAction action)
        {
            var newAddresses = new List<AddressViewModel>(state.Addresses) { action.Address };
            return new CheckoutState(
                selectedProduct: state.SelectedProduct,
                selectedAddress: action.Address,
                addresses: newAddresses,
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: CheckoutState.EvaluateOrderReadiness(action.Address, state.PaymentOption, state.SelectedProduct, state.SelectedCard),
                quantity: state.Quantity,
                totalPrice: state.TotalPrice,
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceSetPaymentOptionAction(CheckoutState state, SetPaymentOptionAction action)
        {
            return new CheckoutState(
                selectedProduct: state.SelectedProduct,
                selectedAddress: state.SelectedAddress,
                addresses: state.Addresses,
                paymentOption: action.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: CheckoutState.EvaluateOrderReadiness(state.SelectedAddress, action.PaymentOption, state.SelectedProduct, state.SelectedCard),
                quantity: state.Quantity,
                totalPrice: state.TotalPrice,
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceSetSelectedCardAction(CheckoutState state, SetSelectedCardAction action)
        {
            return new CheckoutState(
                selectedProduct: state.SelectedProduct,
                selectedAddress: state.SelectedAddress,
                addresses: state.Addresses,
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: action.Card,
                isOrderReady: CheckoutState.EvaluateOrderReadiness(state.SelectedAddress, state.PaymentOption, state.SelectedProduct, action.Card),
                quantity: state.Quantity,
                totalPrice: state.TotalPrice,
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceAddCardSuccessAction(CheckoutState state, AddNewCardSuccessAction action)
        {
            var newCardDetails = new List<CardDetail>(state.CardDetails) { action.Card };
            return new CheckoutState(
                selectedProduct: state.SelectedProduct,
                selectedAddress: state.SelectedAddress,
                addresses: state.Addresses,
                paymentOption: state.PaymentOption,
                cardDetails: newCardDetails,
                selectedCard: action.Card,
                isOrderReady: CheckoutState.EvaluateOrderReadiness(state.SelectedAddress, state.PaymentOption, state.SelectedProduct, action.Card),
                quantity: state.Quantity,
                totalPrice: state.TotalPrice,
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceSetProductAction(CheckoutState state, SetProductAction action)
        {
            // Assuming EvaluateOrderReadiness checks all necessary conditions to be met.
            return new CheckoutState(
                selectedProduct: action.Product,
                selectedAddress: state.SelectedAddress,
                addresses: state.Addresses,
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: CheckoutState.EvaluateOrderReadiness(state.SelectedAddress, state.PaymentOption, action.Product, state.SelectedCard),
                quantity: state.Quantity,
                totalPrice: CalculateTotal(action.Product.Price, state.Quantity),
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceSetAddressesAction(CheckoutState state, SetAddressesAction action)
        {
            return new CheckoutState(
                selectedProduct: state.SelectedProduct,
                selectedAddress: state.SelectedAddress,
                addresses: action.Addresses.ToList(),
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: state.IsOrderReady,
                quantity: state.Quantity,
                totalPrice: state.TotalPrice,
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceSetCardsAction(CheckoutState state, SetCardsAction action)
        {
            return new CheckoutState(
                selectedProduct: state.SelectedProduct,
                selectedAddress: state.SelectedAddress,
                addresses:state.Addresses.ToList(),
                paymentOption: state.PaymentOption,
                cardDetails: action.Cards.ToList(),
                selectedCard: state.SelectedCard,
                isOrderReady: state.IsOrderReady,
                quantity: state.Quantity,
                totalPrice: state.TotalPrice,
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }


        [ReducerMethod]
        public static CheckoutState ReduceUpdateProductQuantityAction(CheckoutState state, UpdateProductQuantityAction action)
        {
            var newTotal = state.SelectedProduct != null ? CalculateTotal(state.SelectedProduct.Price, action.Quantity) : 0;
            return new CheckoutState(
                state.SelectedProduct,
                state.SelectedAddress,
                state.Addresses,
                state.PaymentOption,
                state.CardDetails,
                state.SelectedCard,
                state.IsOrderReady,
                action.Quantity,
                newTotal,
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceFetchProductFailureAction(CheckoutState state, FetchProductFailureAction action)
        {
            return new CheckoutState(
                null,
                state.SelectedAddress,
                state.Addresses,
                state.PaymentOption,
                state.CardDetails,
                state.SelectedCard,
                state.IsOrderReady,
                state.Quantity,
                state.TotalPrice,
                state.IsLoading,
                errorMessage: action.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceFetchFailureAction(CheckoutState state, FetchFailureAction action)
        {
            // Modify the state to include the error message and perhaps set an error state
            return new CheckoutState(
                selectedProduct: state.SelectedProduct,
                selectedAddress: state.SelectedAddress,
                addresses: state.Addresses,
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: state.IsOrderReady,
                quantity: state.Quantity,
                totalPrice: state.TotalPrice,
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        private static decimal CalculateTotal(decimal? price, int quantity)
        {
            return (decimal)(price * quantity);
        }
    }
}
