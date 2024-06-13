namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class FetchFailureAction
    {
        public string ErrorMessage { get; }

        public FetchFailureAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
