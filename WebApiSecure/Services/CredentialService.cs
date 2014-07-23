using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSecure.Classes;
using WebApiSecure.Interfaces;

namespace WebApiSecure.Services
{
    public class CredentialService : IValidateCredential
    {
        public bool IsValidCredential(ICredential credential)
        {
            var user = "";
            if (user == null)
                return false;
            return true;
        }
        public ICredential ParseAuthorizationHeader(string authHeaderParameter)
        {
            string[] credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authHeaderParameter)).Split(':');
            if (credentials.Length != 2 || string.IsNullOrEmpty(credentials[0]) || string.IsNullOrEmpty(credentials[1]))
                return null;
            return new AuthCredential()
            {
                Username = credentials[0],
                Password = credentials[1],
            };
        }
        public IClaim GetClaim(ICredential credential)
        {
            var user = new User();
            return user;
        }
    }
}
