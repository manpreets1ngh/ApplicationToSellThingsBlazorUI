using ApplicationToSellThings.BlazorUI.Models;

namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class SetCardsAction
    {
        public IEnumerable<CardDetail> Cards { get; }

        public SetCardsAction(IEnumerable<CardDetail> cards)
        {
            Cards = cards;
        }
    }
}
