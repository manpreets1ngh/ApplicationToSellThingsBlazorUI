using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationToSellThings.BlazorUI.Middleware
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public TokenAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies["AuthToken"];
            if (!string.IsNullOrEmpty(token))
            {
                // TODO: Validate the token here
                // Extract user information and roles from the token
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var validationParameters = GetTokenValidationParameters();

                    SecurityToken validatedToken;
                    var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                    var userNameClaim = principal.FindFirst(ClaimTypes.Name)?.Value; // or JwtRegisteredClaimNames.Sub
                    var userRoleClaim = principal.FindFirst(ClaimTypes.Role)?.Value; // or a custom claim for roles

                    if (!string.IsNullOrEmpty(userNameClaim) && !string.IsNullOrEmpty(userRoleClaim))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userNameClaim),
                            new Claim(ClaimTypes.Role, userRoleClaim)
                        };

                        var identity = new ClaimsIdentity(claims, "CustomAuthentication");
                        context.User = new ClaimsPrincipal(identity);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    throw ex;
                }
            }

            await _next(context);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])), // Replace with your key
                ValidateIssuer = true,
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:ValidAudience"],
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
