using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel.Security.Tokens;
using System.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using WebApiSecure.Classes;
using WebApiSecure.Interfaces;

namespace WebApiSecure.Services
{
    public class ValidationService : IValidateToken
    {
        public string AllowedTokenRoute { get; set; }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters()
            {
                AllowedAudience = "MyAudience",
                ValidIssuer = "http://www.example.com",
                SigningToken = new BinarySecretSecurityToken(Convert.FromBase64String("SymmetricKey"))

            };
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters);
            return claimsPrincipal;
        }
    }
}
