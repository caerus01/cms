using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Caerus.Common.Web.WebApi.Handlers.Jwt
{
    public class JwtValidationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //return base.SendAsync(request, cancellationToken);

            //Bypass login route (to allow unauthenticated users to login) and bypass Fileupload (which will check its own token)
            if (request.RequestUri.LocalPath.EndsWith("ServiceAuthentication/ServiceLogin") || request.RequestUri.LocalPath.EndsWith("FileUpload"))
                return base.SendAsync(request, cancellationToken);


            string jwtToken;
            if (!TryRetrieveToken(request, out jwtToken))
            {
                HttpResponseMessage response = BuildResponseErrorMessage(HttpStatusCode.Unauthorized);
                return Task.FromResult(response);
            }


            try
            {
                var principal = JwtTokenManager.ValidateJwtToken(jwtToken);

                // set the ClaimsPrincipal on HttpContext.Current if the app is running in web hosted environment.
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }
                else
                {
                    //request.GetRequestContext().Principal = principal;
                    throw new NotImplementedException();
                }
                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException)
            {
                var response = BuildResponseErrorMessage(HttpStatusCode.Unauthorized);
                return Task.FromResult(response);
            }
            catch (Exception)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }


        private HttpResponseMessage BuildResponseErrorMessage(HttpStatusCode statusCode)
        {
            HttpResponseMessage response = new HttpResponseMessage(statusCode);


            // the Scheme should be "Bearer"
            // authorization_uri should point to the tenant url and resource_id should point to the audience
            AuthenticationHeaderValue authenticateHeader = new AuthenticationHeaderValue("Bearer", "authorization_uri=\"" + "unauthorized access!" + "\"" + "," + "resource_id=" + "audience");
            response.Headers.WwwAuthenticate.Add(authenticateHeader);
            return response;
        }


        // Reads the token from the authorization header on the incoming request
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;


            if (!request.Headers.Contains("Authorization"))
            {
                return false;
            }


            string authzHeader = request.Headers.GetValues("Authorization").First<string>();


            // Verify Authorization header contains 'Bearer' scheme
            token = authzHeader.StartsWith("Bearer ") ? authzHeader.Split(' ')[1] : null;


            if (null == token)
            {
                return false;
            }


            return true;
        }
    }
}
