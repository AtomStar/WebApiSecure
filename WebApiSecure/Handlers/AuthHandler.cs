using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebApiSecure.Interfaces;

namespace WebApiSecure.Handlers
{
    public class AuthHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var responseMessage = request.CreateResponse(HttpStatusCode.Unauthorized, "Authentication failed");

            AuthenticationHeaderValue authValue = request.Headers.Authorization;
            var validationService = request.GetDependencyScope().GetService(typeof(IValidateToken)) as IValidateToken;

            if (validationService.IsAllowedRoute(request.RequestUri.PathAndQuery) || request.Method.Method == "OPTIONS" || request.RequestUri.PathAndQuery.Contains("meta"))
                return base.SendAsync(request, cancellationToken);
            if (authValue != null && !String.IsNullOrWhiteSpace(authValue.Parameter))
            {
                try
                {
                    if (authValue.Scheme == "Basic" && request.RequestUri.PathAndQuery.Contains(validationService.AllowedTokenRoute))
                        return base.SendAsync(request, cancellationToken);
                    else if (authValue.Scheme == "Bearer")
                    {
                        var token = authValue.Parameter;
                        var claimsPrincipal = validationService.ValidateToken(token);
                        Thread.CurrentPrincipal = claimsPrincipal;
                        HttpContext.Current.User = claimsPrincipal;
                        return base.SendAsync(request, cancellationToken);
                    }
                    else
                        responseMessage = request.CreateResponse(HttpStatusCode.Unauthorized, "Authentication failed. The authorization header format should be 'Basic myBase64Credential' or 'Bearer myToken'. Also check that you have a valid route (AllowedTokenRoute) for Basic authentication");
                }
                catch (SecurityTokenValidationException)
                { responseMessage = request.CreateResponse(HttpStatusCode.Unauthorized, "Authentication failed. The token is not valid"); }
                catch (Exception ex)
                { responseMessage = request.CreateResponse(HttpStatusCode.InternalServerError, "An exception occured during authentication. " + ex.Message); }
            }
            else
                responseMessage = request.CreateResponse(HttpStatusCode.Unauthorized, "Authentication failed. No authorization header found in the request");
            return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage);
        }

    }
}
