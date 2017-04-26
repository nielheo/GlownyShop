using GlownyShop.Core.Data;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GlownyShop.Auth
{
    public class ProfileService : IProfileService
    {
        protected readonly ILogger Logger;
        IAdminUserRepository _AdminUserRepository;

        public ProfileService(IAdminUserRepository _AdminUserRepository)
        {
            this._AdminUserRepository = _AdminUserRepository;
        }


        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var adminUser = _AdminUserRepository.Get(context.Subject.GetSubjectId(), 
                "AdminUserRoles.AdminUser").Result;

            context.IssuedClaims.Add(new Claim("email", adminUser.Email));
            context.IssuedClaims.Add(new Claim("firstName", adminUser.FirstName));
            context.IssuedClaims.Add(new Claim("lastName", adminUser.LastName));

            if (adminUser.AdminUserRoles.Where(u => u.AdminRoleId == 0).Count() > 0)
                context.IssuedClaims.Add(new Claim("role", "SuperAdmin"));
            //if (context.RequestedClaimTypes.Any())
            //{
            //    context.IssuedClaims.Add(new Claim("role", "superAdmin"));
            //    context.IssuedClaims.Add(new Claim("role", "clientAdmin"));
            //    context.IssuedClaims.Add(new Claim("role", "userAdmin"));
            //}

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var adminUser = _AdminUserRepository.Get(context.Subject.GetSubjectId()).Result;

            context.IsActive = adminUser.IsActive;
            return Task.FromResult(0);
            //throw new NotImplementedException();
        }
    }
}