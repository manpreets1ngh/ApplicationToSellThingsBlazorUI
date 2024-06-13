using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models.Products;
using System.Net;

namespace ApplicationToSellThings.BlazorUI.Store.State
{
    public class CheckoutState
    {
        public ProductViewModel SelectedProduct { get; }
        public AddressViewModel SelectedAddress { get; }
        public IEnumerable<AddressViewModel> Addresses { get; }
        public string PaymentOption { get; }
        public IEnumerable<CardDetail> CardDetails { get; }
        public CardDetail SelectedCard { get; }
        public bool IsOrderReady { get; }
        public int Quantity { get; private set; } 
        public decimal TotalPrice { get; private set; }
        public bool IsLoading { get; private set; }
        public string ErrorMessage { get; private set; }
        public CheckoutState(
            ProductViewModel selectedProduct,
            AddressViewModel selectedAddress,
            IEnumerable<AddressViewModel> addresses,
            string paymentOption,
            IEnumerable<CardDetail> cardDetails,
            CardDetail selectedCard,
            bool isOrderReady,
            int quantity,
        decimal totalPrice,
        bool isLoading,
        string errorMessage)
        {
            SelectedProduct = selectedProduct;
            SelectedAddress = selectedAddress;
            Addresses = addresses;
            PaymentOption = paymentOption;
            CardDetails = cardDetails;
            SelectedCard = selectedCard;
            IsOrderReady = isOrderReady;
            Quantity = quantity;
            TotalPrice = totalPrice;
            IsLoading= isLoading;
            ErrorMessage = errorMessage;
        }

        // Method to check if the order is ready to be placed
        public static bool EvaluateOrderReadiness(AddressViewModel address, string paymentOption, ProductViewModel product, CardDetail card = null)
        {
            // Simple logic to check all conditions are met
            return address != null && !string.IsNullOrEmpty(paymentOption) && product != null && (paymentOption != "card" || (paymentOption == "card" && card != null));
        }
    }
}
