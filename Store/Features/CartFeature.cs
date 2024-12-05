using ApplicationToSellThings.BlazorUI.Models;
using ApplicationToSellThings.BlazorUI.Store.State;
using Fluxor;

namespace ApplicationToSellThings.BlazorUI.Store.Features
{
    public class CartFeature : Feature<CartState>
    {
        public override string GetName() => "Cart";

        protected override CartState GetInitialState() => CartState.InitialCartState;
    }
}