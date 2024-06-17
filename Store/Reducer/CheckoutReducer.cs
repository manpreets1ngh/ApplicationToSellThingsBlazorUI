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
        public static CheckoutState ReduceSetSelectedProductsAction(CheckoutState state, SetSelectedProductsAction action)
        {
            var quantities = action.Products.ToDictionary(p => p.ProductId.ToString(), p => p.QuantityInStock);

            return new CheckoutState(
                selectedProducts: action.Products,
                selectedAddress: state.SelectedAddress,
                addresses: state.Addresses,
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: state.EvaluateOrderReadiness(state.SelectedAddress, state.PaymentOption, action.Products, state.SelectedCard),
                quantities: quantities,
                totalPrice: state.SelectedProducts.Sum(p => p.Price * state.Quantities[p.ProductId.ToString()]),
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }
        
        [ReducerMethod]
        public static CheckoutState ReduceSetSelectedAddressAction(CheckoutState state, SetSelectedAddressAction action)
        {
            return new CheckoutState(
                selectedProducts: state.SelectedProducts,
                selectedAddress: action.Address,
                addresses: state.Addresses,
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: state.EvaluateOrderReadiness(action.Address, state.PaymentOption, state.SelectedProducts, state.SelectedCard),
                quantities: state.Quantities,
                totalPrice: state.SelectedProducts.Sum(p => p.Price * state.Quantities[p.ProductId.ToString()]),
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceAddAddressSuccessAction(CheckoutState state, AddAddressSuccessAction action)
        {
            var newAddresses = new List<AddressViewModel>(state.Addresses) { action.Address };
            return new CheckoutState(
                selectedProducts: state.SelectedProducts,
                selectedAddress: action.Address,
                addresses: newAddresses,
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: state.EvaluateOrderReadiness(action.Address, state.PaymentOption, state.SelectedProducts, state.SelectedCard),
                quantities: state.Quantities,
                totalPrice: state.SelectedProducts.Sum(p => p.Price * state.Quantities[p.ProductId.ToString()]),
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceSetPaymentOptionAction(CheckoutState state, SetPaymentOptionAction action)
        {
            return new CheckoutState(
                selectedProducts: state.SelectedProducts,
                selectedAddress: state.SelectedAddress,
                addresses: state.Addresses,
                paymentOption: action.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: state.EvaluateOrderReadiness(state.SelectedAddress, action.PaymentOption, state.SelectedProducts, state.SelectedCard),
                quantities: state.Quantities,
                totalPrice: state.SelectedProducts.Sum(p => p.Price * state.Quantities[p.ProductId.ToString()]),
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceSetSelectedCardAction(CheckoutState state, SetSelectedCardAction action)
        {
            return new CheckoutState(
                selectedProducts: state.SelectedProducts,
                selectedAddress: state.SelectedAddress,
                addresses: state.Addresses,
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: action.Card,
                isOrderReady: state.EvaluateOrderReadiness(state.SelectedAddress, state.PaymentOption, state.SelectedProducts, action.Card),
                quantities: state.Quantities,
                totalPrice: state.SelectedProducts.Sum(p => p.Price * state.Quantities[p.ProductId.ToString()]),
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceAddCardSuccessAction(CheckoutState state, AddNewCardSuccessAction action)
        {
            var newCardDetails = new List<CardDetail>(state.CardDetails) { action.Card };
            return new CheckoutState(
                selectedProducts: state.SelectedProducts,
                selectedAddress: state.SelectedAddress,
                addresses: state.Addresses,
                paymentOption: state.PaymentOption,
                cardDetails: newCardDetails,
                selectedCard: action.Card,
                isOrderReady: state.EvaluateOrderReadiness(state.SelectedAddress, state.PaymentOption, state.SelectedProducts, action.Card),
                quantities: state.Quantities,
                totalPrice: state.SelectedProducts.Sum(p => p.Price * state.Quantities[p.ProductId.ToString()]),
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceSetProductsAction(CheckoutState state, SetProductsAction action)
        {
            var quantities = action.Products.ToDictionary(p => p.ProductId.ToString(), p => p.QuantityInStock);

            return new CheckoutState(
                selectedProducts: action.Products,
                selectedAddress: state.SelectedAddress,
                addresses: state.Addresses,
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: state.EvaluateOrderReadiness(state.SelectedAddress, state.PaymentOption, action.Products, state.SelectedCard),
                quantities: state.Quantities,
                totalPrice: state.SelectedProducts.Sum(p => p.Price * state.Quantities[p.ProductId.ToString()]),
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceSetAddressesAction(CheckoutState state, SetAddressesAction action)
        {
            return new CheckoutState(
                selectedProducts: state.SelectedProducts,
                selectedAddress: state.SelectedAddress,
                addresses: action.Addresses.ToList(),
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: state.EvaluateOrderReadiness(state.SelectedAddress, state.PaymentOption, state.SelectedProducts, state.SelectedCard),
                quantities: state.Quantities,
                totalPrice: state.SelectedProducts.Sum(p => p.Price * state.Quantities[p.ProductId.ToString()]),
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceSetCardsAction(CheckoutState state, SetCardsAction action)
        {
            return new CheckoutState(
                selectedProducts: state.SelectedProducts,
                selectedAddress: state.SelectedAddress,
                addresses:state.Addresses.ToList(),
                paymentOption: state.PaymentOption,
                cardDetails: action.Cards.ToList(),
                selectedCard: state.SelectedCard,
                isOrderReady: state.EvaluateOrderReadiness(state.SelectedAddress, state.PaymentOption, state.SelectedProducts, state.SelectedCard),
                quantities: state.Quantities,
                totalPrice: state.SelectedProducts.Sum(p => p.Price * state.Quantities[p.ProductId.ToString()]),
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceUpdateProductQuantityAction(CheckoutState state, UpdateProductQuantityAction action)
        {
            var quantities = new Dictionary<string, int>(state.Quantities)
            {
                [action.ProductId.ToString()] = action.Quantity
            };

            var totalPrice = state.SelectedProducts.Sum(p => p.Price * state.Quantities[p.ProductId.ToString()]);


            return new CheckoutState(
                state.SelectedProducts,
                state.SelectedAddress,
                state.Addresses,
                state.PaymentOption,
                state.CardDetails,
                state.SelectedCard,
                state.IsOrderReady,
                quantities,
                totalPrice,
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
                isOrderReady: state.EvaluateOrderReadiness(state.SelectedAddress, state.PaymentOption, null, state.SelectedCard),
                quantities: state.Quantities,
                totalPrice: state.SelectedProducts.Sum(p => p.Price * state.Quantities[p.ProductId.ToString()]),
                state.IsLoading,
                errorMessage: action.ErrorMessage
            );
        }

        [ReducerMethod]
        public static CheckoutState ReduceFetchFailureAction(CheckoutState state, FetchFailureAction action)
        {
            // Modify the state to include the error message and perhaps set an error state
            return new CheckoutState(
                selectedProducts: state.SelectedProducts,
                selectedAddress: state.SelectedAddress,
                addresses: state.Addresses,
                paymentOption: state.PaymentOption,
                cardDetails: state.CardDetails,
                selectedCard: state.SelectedCard,
                isOrderReady: state.EvaluateOrderReadiness(state.SelectedAddress, state.PaymentOption, state.SelectedProducts, state.SelectedCard),
                quantities: state.Quantities,
                totalPrice: state.SelectedProducts.Sum(p => p.Price * state.Quantities[p.ProductId.ToString()]),
                isLoading: false,
                errorMessage: state.ErrorMessage
            );
        }

        private static decimal CalculateTotal(decimal? price, int quantity)
        {
            return (decimal)(price * quantity);
        }
        
        [ReducerMethod]
        public static CheckoutState ReduceUpdateCheckoutStateAction(CheckoutState state, UpdateCheckoutStateAction action)
        {
            return action.State;
        }
    }
}
