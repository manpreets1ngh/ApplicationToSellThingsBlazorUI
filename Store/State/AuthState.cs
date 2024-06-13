namespace ApplicationToSellThings.BlazorUI.Store.State
{
    public class AuthState
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<string> UserRoles { get; set; }

        public AuthState(bool isAuthenticated, string token, string userName, string email, List<string> userRoles)
        {
            IsAuthenticated = isAuthenticated;
            Token = token;
            UserName = userName;
            EmailAddress = email;
            UserRoles = userRoles;
        }

    }
}
