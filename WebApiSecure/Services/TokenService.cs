using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSecure.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace WebApiSecure.Services
{
    public class TokenService : ITokenBuilder
    {
        public string GetToken(IClaim credentialClaim)
        {
            var token = new JwtSecurityToken
                     (issuer: "http://www.example.com"
                     , audience: "MyAudience"
                     , claims: credentialClaim.GetClaims()
                     , notBefore: DateTime.UtcNow
                     , expires: DateTime.UtcNow.AddHours(24)
                     , signingCredentials: CreateSigningCredentials());

            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        private SigningCredentials CreateSigningCredentials()
        {
            string secretKey = "MySuperSecretKey";
            byte[] keybytes = Encoding.ASCII.GetBytes(secretKey);
            SecurityKey securityKey = new SymmetricSecurityKey(keybytes);
            SigningCredentials signingCredentials =
                    new SigningCredentials(securityKey,
                        SecurityAlgorithms.HmacSha256Signature);
            return signingCredentials;
        }
    }
}
