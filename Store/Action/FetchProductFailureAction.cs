namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class FetchProductFailureAction
    {
        public string ErrorMessage { get; }
        public FetchProductFailureAction(string errorMessage) => ErrorMessage = errorMessage;
    }
}
