using ApplicationToSellThings.BlazorUI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ApplicationToSellThings.BlazorUI.Helper
{
    public class UserDetailHelper
    {
        public UserDetail GetUserDetailFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var userId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "jti")?.Value;
            var email = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;
            var username = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
            var userDetail = new UserDetail()
            {
                Id = userId,
                Email = email,
                UserName = username
            };

            return userDetail;
        }
    }
}
