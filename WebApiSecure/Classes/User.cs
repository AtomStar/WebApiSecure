using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSecure.Interfaces;
using System.Security.Claims;

namespace WebApiSecure.Classes
{
    public class User : IClaim
    {
        public List<Claim> GetClaims()
        {
            return new List<Claim>();
        }
    }
}
