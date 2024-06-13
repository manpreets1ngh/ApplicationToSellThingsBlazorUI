namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class FetchCardsForUserAction
    {
        public Guid UserId { get; }
        public FetchCardsForUserAction(Guid userId) => UserId = userId;
    }
}
