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
            var adminUser = _SecurityService.ValidateAdminUser(context.UserName, context.Password);
            if (adminUser != null)
            {
                ICollection<Claim> claims = new HashSet<Claim>(new ClaimComparer());

                claims.Add(new Claim("email", adminUser.Email));
                claims.Add(new Claim("firstName", adminUser.FirstName));
                claims.Add(new Claim("lastName", adminUser.LastName));

                adminUser = _AdminUserRepository.Get(adminUser.Id, "AdminUserRoles.AdminUser").Result;

                if (adminUser.AdminUserRoles.Where(u => u.AdminRoleId == 0).Count() > 0)
                    claims.Add(new Claim("role", "SuperAdmin"));

                context.Result = new GrantValidationResult(adminUser.Id,
                    OidcConstants.AuthenticationMethods.Password, claims);
                
            }

            return Task.FromResult(0);
        }
    }
}