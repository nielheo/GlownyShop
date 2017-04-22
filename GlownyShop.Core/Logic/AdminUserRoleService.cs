using System;
using System.Collections.Generic;
using System.Text;
using GlownyShop.Models;
using GlownyShop.Core.Data;
using System.Threading.Tasks;

namespace GlownyShop.Core.Logic
{
    public class AdminUserRoleService : IAdminUserRoleService
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IAdminRoleRepository _adminRoleRepository;

        public AdminUserRoleService(IAdminUserRepository adminUserRepository,
            IAdminRoleRepository adminRoleRepository)
        {
            _adminUserRepository = adminUserRepository;
            _adminRoleRepository = adminRoleRepository;
        }

        public Task<AdminUser> AddAdminUser(AdminUser adminUser)
        {
            var adminUserExist = _adminUserRepository.GetByEmail(adminUser.Email).Result;

            if (adminUserExist != null)
                throw new ArgumentException(string.Format("User with Email {0} already used", adminUser.Email), "email");

            _adminUserRepository.Add(adminUser);
            var saved = _adminUserRepository.SaveChangesAsync().Result;

            if (!saved)
                return null;

            return _adminUserRepository.Get(adminUser.Id);
        }

        public Task<AdminUser> UpdateAdminUser(AdminUser adminUser)
        {
            var dbAdminUser = _adminUserRepository.Get(adminUser.Id).Result;

            if (dbAdminUser == null)
                throw new Exception(string.Format("User with id: {0}, not found", adminUser.Id));

            var adminUserExist = _adminUserRepository.GetByEmail(adminUser.Email).Result;

            if (adminUserExist != null && adminUserExist.Id != adminUser.Id)
                throw new ArgumentException(string.Format("User with Email {0} already used", adminUser.Email), "email");

            dbAdminUser = _adminUserRepository.Get(adminUser.Id).Result;
            dbAdminUser.Email = adminUser.Email;
            dbAdminUser.FirstName = adminUser.FirstName;
            dbAdminUser.LastName = adminUser.LastName;
            dbAdminUser.IsActive = adminUser.IsActive;
            
            var saved = _adminUserRepository.SaveChangesAsync().Result;

            if (!saved)
                return null;

            return _adminUserRepository.Get(adminUser.Id);
        }
    }
}
