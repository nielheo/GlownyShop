using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GlownyShop.Auth
{
    public class ProfileService : IProfileService
    {
        protected readonly ILogger Logger;

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //if (context.RequestedClaimTypes.Any())
            //{
            context.IssuedClaims.Add(new Claim("role", "superAdmin"));
            context.IssuedClaims.Add(new Claim("role", "clientAdmin"));
            context.IssuedClaims.Add(new Claim("role", "userAdmin"));
            //}

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.FromResult(0);
            //throw new NotImplementedException();
        }
    }
}