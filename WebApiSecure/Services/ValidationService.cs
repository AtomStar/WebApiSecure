using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiSecure.Classes;
using WebApiSecure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace WebApiSecure.Services
{
    public class ValidationService : IValidateToken
    {
        public string AllowedTokenRoute { get; set; }
        public IEnumerable<string> AllowedRoutes { get; set; }
        public bool IsAllowedRoute(string route)
        {
            foreach (var item in AllowedRoutes)
            {
                if (route.ToLower().EndsWith(item.ToLower()))
                    return true;
            }
            return false;      
        }
        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;
            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SymmetricKey")),

                ValidateAudience = true,
                ValidAudience = "MyAudience",

                ValidateIssuer = true,
                ValidIssuer = "http://www.example.com",
                ValidateLifetime = true
            };

            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return claimsPrincipal;
        }
    }
}
