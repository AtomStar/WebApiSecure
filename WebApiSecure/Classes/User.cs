using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSecure.Interfaces;

namespace WebApiSecure.Classes
{
    public class User:IClaim
    {
        public List<System.Security.Claims.Claim> GetClaims()
        {
            throw new NotImplementedException();
        }
    }
}
