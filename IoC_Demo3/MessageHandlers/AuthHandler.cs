using Interfaces;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.IdentityModel.Configuration;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace IoC_Demo3.MessageHandlers
{
    public class AuthHandler : DelegatingHandler
    {
        public AuthHandler()
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // check for a user token in the cache, if found then decrypted and create principle to be added to the current thread
            var cachedToken = HttpContext.Current.Cache["authToken"];
            if (cachedToken != null)
            {
                var cryptoService = ServiceLocator.Current.GetInstance<ICryptoService>();
                var authService = ServiceLocator.Current.GetInstance<IAuthenticationService>();

                // decrypt user token
                var decryptedData = cryptoService.Decrypt(cachedToken.ToString());
                var userpass = decryptedData.Split('\\');

                // check for the user on the database
                var user = authService.IsAuthorized(userpass[0], userpass[1]);

                // create claims, identity and principle
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email)
                    };
                    foreach(var role in user.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                    }

                    var id = new ClaimsIdentity(claims, "EncryptedAuthentication");
                    var principal = new ClaimsPrincipal(id);

                    var config = new IdentityConfiguration();
                    var newPrincipal = config.ClaimsAuthenticationManager.Authenticate(request.RequestUri.ToString(), principal);


                    // add principal to the current thread, http context
                    Thread.CurrentPrincipal = newPrincipal;
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = newPrincipal;
                    }
                }
            }


            return base.SendAsync(request, cancellationToken);
        }
    }
}