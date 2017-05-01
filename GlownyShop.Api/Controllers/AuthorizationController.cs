using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using GlownyShop.Core.Logic;
using GlownyShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace GlownyShop.Api.Controllers
{
    public class AuthorizationController : Controller
    {
        private ISecurityService _securityService;

        public AuthorizationController(ISecurityService securityService)
        {
            _securityService = securityService;
        }
        
        [HttpPost("/connect/token"), Produces("application/json")]
        public IActionResult Exchange(OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                // Validate the user credentials.
                // Note: to mitigate brute force attacks, you SHOULD strongly consider
                // applying a key derivation function like PBKDF2 to slow down
                // the password validation process. You SHOULD also consider
                // using a time-constant comparer to prevent timing attacks.
                AdminUser adminUser = _securityService.ValidateAdminUser(request.Username, request.Password);
                if (adminUser == null)
                {
                    return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
                }
                // Create a new ClaimsIdentity holding the user identity.
                var identity = new ClaimsIdentity(
                    OpenIdConnectServerDefaults.AuthenticationScheme,
                    OpenIdConnectConstants.Claims.Name,
                    OpenIdConnectConstants.Claims.Role);
                // Add a "sub" claim containing the user identifier, and attach
                // the "access_token" destination to allow OpenIddict to store it
                // in the access token, so it can be retrieved from your controllers.
                identity.AddClaim(OpenIdConnectConstants.Claims.Subject,
                    adminUser.Id,
                    OpenIdConnectConstants.Destinations.AccessToken);
                identity.AddClaim(OpenIdConnectConstants.Claims.Name, 
                    string.Format("{0} {1}", adminUser.FirstName, adminUser.LastName),
                    OpenIdConnectConstants.Destinations.AccessToken);

                foreach (var adminRoles in _securityService.GetAdminRoles(adminUser.Id))
                {
                    identity.AddClaim(OpenIdConnectConstants.Claims.Role,
                        adminRoles.Name, 
                        OpenIdConnectConstants.Destinations.AccessToken);
                }
                // ... add other claims, if necessary.
                var principal = new ClaimsPrincipal(identity);
                // Ask OpenIddict to generate a new token and return an OAuth2 token response.
                return SignIn(principal, OpenIdConnectServerDefaults.AuthenticationScheme);
            }
            throw new InvalidOperationException("The specified grant type is not supported.");
        }
    }
}