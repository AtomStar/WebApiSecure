using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSecure.Interfaces
{
    public interface IValidateCredential
    {        
        bool IsValidCredential(ICredential credential);
        IClaim GetClaim(ICredential credential);
        ICredential ParseAuthorizationHeader(string authHeaderParameter);
    }
}
