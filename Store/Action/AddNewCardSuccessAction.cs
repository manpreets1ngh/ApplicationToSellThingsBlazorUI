using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class AddNewCardSuccessAction
    {
        public CardDetail Card { get; }
        public AddNewCardSuccessAction(CardDetail card) => Card = card;
    }
}
