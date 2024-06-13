namespace ApplicationToSellThings.BlazorUI.Store.Action
{
    public class LoginAction
    {
        public string Token { get; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> UserRoles { get; set; }
        public LoginAction(string token, string userName, string email, List<string> userRoles)
        {
            Token = token;
            UserName = userName;
            Email = email;
            UserRoles = userRoles;
        }
    }
}
