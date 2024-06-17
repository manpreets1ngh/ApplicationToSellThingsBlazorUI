using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Models.Products;
using System.Net;

namespace ApplicationToSellThings.BlazorUI.Store.State
{
    public class CheckoutState
    {
        public List<ProductViewModel> SelectedProducts { get; set; }
        public AddressViewModel SelectedAddress { get; }
        public IEnumerable<AddressViewModel> Addresses { get; }
        public string PaymentOption { get; }
        public IEnumerable<CardDetail> CardDetails { get; }
        public CardDetail? SelectedCard { get; }
        public bool IsOrderReady { get; set; }
        public Dictionary<string, int> Quantities { get; set; }
        public decimal TotalPrice { get; private set; }
        public bool IsLoading { get; private set; }
        public string ErrorMessage { get; private set; }
        public CheckoutState(
            List<ProductViewModel> selectedProducts,
            AddressViewModel selectedAddress,
            IEnumerable<AddressViewModel> addresses,
            string paymentOption,
            IEnumerable<CardDetail> cardDetails,
            CardDetail selectedCard,
            bool isOrderReady,
            Dictionary<string, int> quantities,
            decimal totalPrice,
            bool isLoading,
            string errorMessage)
        {
            SelectedProducts = selectedProducts;
            SelectedAddress = selectedAddress;
            Addresses = addresses;
            PaymentOption = paymentOption;
            CardDetails = cardDetails;
            SelectedCard = selectedCard;
            IsOrderReady = isOrderReady;
            Quantities = quantities;
            TotalPrice = totalPrice;
            IsLoading= isLoading;
            ErrorMessage = errorMessage;
        }

        public bool EvaluateOrderReadiness(AddressViewModel address, string paymentOption, List<ProductViewModel> products, CardDetail card = null)
        {
            return address != null && !string.IsNullOrEmpty(paymentOption) && products.Any() && (paymentOption != "card" || (paymentOption == "card" && card != null));
        }
    }
}
