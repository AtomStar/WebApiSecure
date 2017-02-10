using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace WebApiSecure.Filters
{
    //Only a tagging filter for Swagger integration
    public class RequireTokenAttribute: ActionFilterAttribute
    {
        
    }
}
