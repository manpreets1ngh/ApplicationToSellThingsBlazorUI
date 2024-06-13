using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class SetSelectedCardAction
    {
        public CardDetail Card { get; }
        public SetSelectedCardAction(CardDetail card) => Card = card;
    }
}
