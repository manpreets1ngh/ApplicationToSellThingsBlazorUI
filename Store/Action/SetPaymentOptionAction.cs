namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class SetPaymentOptionAction
    {
        public string PaymentOption { get; }
        public SetPaymentOptionAction(string paymentOption) => PaymentOption = paymentOption;
    }
}
