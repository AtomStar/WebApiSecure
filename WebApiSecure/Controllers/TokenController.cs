using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiSecure.Classes;
using WebApiSecure.Filters;
using WebApiSecure.Interfaces;

namespace WebApiSecure.Controllers
{
    public class TokenController: ApiController
    {
        [RequireHttps]
        public IHttpActionResult PostSecure(string grantType)
        {
            var headers = Request.Headers;

            if (headers != null && headers.Authorization != null && headers.Authorization.Scheme == "Basic")
            {
                if (String.IsNullOrWhiteSpace(grantType))
                    return BadRequest("Invalid request");
                else if (grantType != "client_credentials")
                    return BadRequest("Invalid grant type");
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
                catch (FormatException)
                {
                    return BadRequest("Invalid authorization header format");
                }
                catch (Exception)
                {
                    return BadRequest("Internal server error");
                }
            }
            return BadRequest("Invalid authorization header");
        }

        public IHttpActionResult Post(string grantType)
        {
            var headers = Request.Headers;

            if (headers != null && headers.Authorization != null && headers.Authorization.Scheme == "Basic")
            {
                if (String.IsNullOrWhiteSpace(grantType))
                    return BadRequest("Invalid request");
                else if (grantType != "client_credentials")
                    return BadRequest("Invalid grant type");
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
                catch (FormatException)
                {
                    return BadRequest("Invalid authorization header format");
                }
                catch (Exception)
                {
                    return BadRequest("Internal server error");
                }
            }
            return BadRequest("Invalid authorization header");
        }

    }
}
