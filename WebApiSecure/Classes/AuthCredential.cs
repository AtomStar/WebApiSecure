using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSecure.Interfaces;

namespace WebApiSecure.Classes
{
    public class AuthCredential : ICredential
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
