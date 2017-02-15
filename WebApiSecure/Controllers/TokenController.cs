using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiSecure.Classes;
using WebApiSecure.Filters;
using WebApiSecure.Interfaces;

namespace WebApiSecure.Controllers
{
    public class TokenController : ApiController
    {
        [RequireHttps]
        [RequireToken]
        public IHttpActionResult PostSecure()
        {
            return CreateToken(Request.Headers);
        }
        [RequireToken]
        public IHttpActionResult Post()
        {
            return CreateToken(Request.Headers);
        }
        private IHttpActionResult CreateToken(HttpRequestHeaders headers)
        {
            if (headers != null && headers.Authorization != null && headers.Authorization.Scheme == "Basic")
            {
                try
                {
                    var credentialValidator = Configuration.DependencyResolver.GetService(typeof(IValidateCredential)) as IValidateCredential;
                    var tokenBuilder = Configuration.DependencyResolver.GetService(typeof(ITokenBuilder)) as ITokenBuilder;
                    var credential = credentialValidator.ParseAuthorizationHeader(headers.Authorization.Parameter);

                    if (credentialValidator.IsValidCredential(credential))
                    {
                        var credentialClaim = credentialValidator.GetClaim(credential);
                        string tokenString = tokenBuilder.GetToken(credentialClaim);
                        return Ok<AuthToken>(new AuthToken(tokenString));
                    }
                    else
                        return BadRequest("Invalid client credentials");
                }
                catch (FormatException exf)
                {
                    return BadRequest("Invalid authorization header format." + exf.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest("Internal server error" + ex.Message);
                }
            }
            return BadRequest("Invalid authorization header. The authorization header has to be set an in the format 'Basic myBase64Credential'");
        }
    }
}
