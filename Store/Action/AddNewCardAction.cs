using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class AddNewCardAction
    {
        public CardRequestModel Card { get; }
        public AddNewCardAction(CardRequestModel card) => Card = card;
    }
}
