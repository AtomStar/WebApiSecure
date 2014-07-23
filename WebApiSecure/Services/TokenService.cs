using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSecure.Interfaces;

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
                     , lifetime: new Lifetime(DateTime.UtcNow, DateTime.UtcNow.AddHours(24))
                     , signingCredentials: CreateSigningCredentials());

            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        private SigningCredentials CreateSigningCredentials()
        {
            string symmetricKey = "SymmetricKey";
            byte[] keybytes = Convert.FromBase64String(symmetricKey);
            SecurityKey securityKey = new InMemorySymmetricSecurityKey(keybytes);
            SigningCredentials signingCredentials =
                    new SigningCredentials(securityKey,
                        SecurityAlgorithms.HmacSha256Signature,
                        SecurityAlgorithms.Sha256Digest);
            return signingCredentials;
        }
    }
}
