using ApplicationToSellThings.BlazorUI.Store.Action;
using ApplicationToSellThings.BlazorUI.Store.State;
using Fluxor;

namespace ApplicationToSellThings.BlazorUI.Store.Reducer
{
    public class AuthReducer
    {
        [ReducerMethod]
        public static AuthState ReduceLoginAction(AuthState state, LoginAction action)
        {
            return new AuthState(true, action.Token, action.UserName, action.Email, action.UserRoles );
        }

        [ReducerMethod]
        public static AuthState ReduceLogoutAction(AuthState state, LogoutAction action)
        {
            return new AuthState(false, null, null, null, null );
        }
    }
}
