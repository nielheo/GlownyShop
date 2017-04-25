using System.Threading.Tasks;
using IdentityServer4.Validation;
using IdentityModel;
using GlownyShop.Core.Logic;
using GlownyShop.Core.Data;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace GlownyShop.Auth
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        ISecurityService _SecurityService;
        IAdminUserRepository _AdminUserRepository;

        public ResourceOwnerPasswordValidator (ISecurityService _SecurityService,
            IAdminUserRepository _AdminUserRepository)
        {
            this._SecurityService = _SecurityService;
            this._AdminUserRepository = _AdminUserRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_SecurityService.ValidateAdminUser(context.UserName, context.Password))
            {
                var adminUser = _AdminUserRepository.GetAll().Result.FirstOrDefault();
                ICollection<Claim> claims = new HashSet<Claim>(new ClaimComparer());

                claims.Add(new Claim("email", adminUser.Email));
                claims.Add(new Claim("firstName", adminUser.FirstName));
                claims.Add(new Claim("lastName", adminUser.LastName));
                
                context.Result = new GrantValidationResult(adminUser.Id,
                    OidcConstants.AuthenticationMethods.Password, claims);
                
            }

            return Task.FromResult(0);
        }
    }
}