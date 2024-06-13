using ApplicationToSellThings.BlazorUI.Store.State;
using Fluxor;

namespace ApplicationToSellThings.BlazorUI.Store.Features
{
    public class AuthFeature : Feature<AuthState>
    {
        public override string GetName() => "Auth";

        protected override AuthState GetInitialState() =>
            new AuthState(false, null, null, null, null);
    }
}
