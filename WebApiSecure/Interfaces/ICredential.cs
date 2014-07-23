using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSecure.Interfaces
{
    public interface ICredential
    {
        string Username { get; set; }
        string Password { get; set; }
    }
}
