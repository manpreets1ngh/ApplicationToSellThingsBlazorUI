namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class AddAddressFailureAction
    {
        public string ErrorMessage { get; }

        public AddAddressFailureAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
