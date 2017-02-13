using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSecure.Interfaces
{
    public interface IValidateToken
    {
        IEnumerable<string> AllowedRoutes { get; set; }
        string AllowedTokenRoute { get; set; }
        bool IsAllowedRoute(string route);
        ClaimsPrincipal ValidateToken(string token);
    }
}
