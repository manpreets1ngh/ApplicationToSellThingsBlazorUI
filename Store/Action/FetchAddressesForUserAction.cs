namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class FetchAddressesForUserAction
    {
        public Guid UserId { get; }
        public FetchAddressesForUserAction(Guid userId) => UserId = userId;
    }
}
