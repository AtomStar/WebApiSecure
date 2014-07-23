using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSecure.Interfaces
{
    public interface IClaim
    {
        List<Claim> GetClaims();
    }
}
