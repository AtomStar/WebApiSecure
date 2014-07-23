using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSecure.Classes
{
    public class AuthToken
    {
        public string Token { get; set; }
        public AuthToken(string token)
        {
            this.Token = token;
        }
    }
}
