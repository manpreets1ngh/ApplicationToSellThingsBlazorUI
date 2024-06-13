using ApplicationToSellThings.BlazorUI.Services.Interfaces;
using ApplicationToSellThings.BlazorUI.Store.Action;
using Fluxor;

namespace ApplicationToSellThings.BlazorUI.Store.Effects
{
    public class CheckoutEffects
    {
        private readonly IAddressService _addressService;
        private readonly IProductsService _productService;
        private readonly ICardService _cardService;

        public CheckoutEffects(IAddressService addressService, ICardService cardService, IProductsService productService)
        {
            _addressService = addressService;
            _cardService = cardService;
            _productService = productService;
        }

        [EffectMethod]
        public async Task HandleAddNewAddressAsync(AddNewAddressAction action, IDispatcher dispatcher)
        {
            try
            {
                var addressResponse = await _addressService.AddNewAddress(action.Address);
                if (addressResponse != null)
                    dispatcher.Dispatch(new AddAddressSuccessAction(addressResponse));
                else
                    dispatcher.Dispatch(new AddAddressFailureAction("Failed to add address - response null"));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new AddAddressFailureAction($"Failed to add address: {ex.Message}"));
            }
        }

        [EffectMethod]
        public async Task HandleAddNewCardAsync(AddNewCardAction action, IDispatcher dispatcher)
        {
            var cardResponse = await _cardService.AddNewCardForUser(action.Card);
            dispatcher.Dispatch(new AddNewCardSuccessAction(cardResponse));
        }

        [EffectMethod]
        public async Task HandleFetchProductAsync(FetchProductAction action, IDispatcher dispatcher)
        {
            try
            {
                var product = await _productService.GetProductByProductId(action.ProductId);
                dispatcher.Dispatch(new SetProductAction(product));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new FetchProductFailureAction(ex.Message));
            }
        }

       

        [EffectMethod]
        public async Task HandleFetchAddressesForUser(FetchAddressesForUserAction action, IDispatcher dispatcher)
        {
            
            try
            {
                var addresses = await _addressService.GetAddressByUser(action.UserId);
                dispatcher.Dispatch(new SetAddressesAction(addresses));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new FetchFailureAction(ex.Message)); // Handle failure appropriately
            }
        }

        [EffectMethod]
        public async Task HandleFetchCardsForUser(FetchCardsForUserAction action, IDispatcher dispatcher)
        {
            try
            {
                var cards = await _cardService.GetCardDetailsForUser(action.UserId);
                dispatcher.Dispatch(new SetCardsAction(cards));
            }
            catch(Exception ex)
            {
                dispatcher.Dispatch(new FetchFailureAction(ex.Message)); // Handle failure appropriately    
            }
        }

    }
}
