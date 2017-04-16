using GlownyShop.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace GlownyShop.Data.EntityFramework.Seed
{
    public static class GlownyShopSeedData
    {
        public static void EnsureSeedData(this GlownyShopContext db)
        {
            db._logger.LogInformation("Seeding database");

            // admin Roles
            var AdminRoleSuperAdmin = new AdminRole { Id = 0, Name = "Super Admin" };
            var AdminRoleUserAdmin = new AdminRole { Id = 1, Name = "User Admin" };
            var AdminRoleClientAdmin = new AdminRole { Id = 2, Name = "Client Admin" };

            var adminRoles = new List<AdminRole>
            {
                AdminRoleSuperAdmin,
                AdminRoleUserAdmin,
                AdminRoleClientAdmin,
            };
            if (!db.AdminRoles.Any())
            {
                db._logger.LogInformation("Seeding admin roles");
                db.AdminRoles.AddRange(adminRoles);
                db.SaveChanges();
            }

            // admin users
            var AdminUserSuperAdmin = new AdminUser
            {
                //Id = 0,
                FirstName = "Super",
                LastName = "Admin",
                Email = "superadmin@glowny-shop.com",
                IsActive = true,
                AdminUserRoles = new List<AdminUserRole>
                {
                    new AdminUserRole { AdminRole = AdminRoleSuperAdmin }
                }
            };

            var AdminUserUserAdmin = new AdminUser
            {
                //Id = 1,
                FirstName = "User",
                LastName = "Admin",
                Email = "useradmin@glowny-shop.com",
                IsActive = true,
                AdminUserRoles = new List<AdminUserRole>
                {
                    new AdminUserRole { AdminRole = AdminRoleUserAdmin }
                }
            };

            var AdminUserClientAdmin = new AdminUser
            {
                //Id = 2,
                FirstName = "Client",
                LastName = "Admin",
                Email = "clientadmin@glowny-shop.com",
                IsActive = true,
                AdminUserRoles = new List<AdminUserRole>
                {
                    new AdminUserRole { AdminRole = AdminRoleClientAdmin }
                }
            };

            var adminUsers = new List<AdminUser>
            {
                AdminUserSuperAdmin,
                AdminUserUserAdmin,
                AdminUserClientAdmin,
            };
            if (!db.AdminUsers.Any())
            {
                db._logger.LogInformation("Seeding admin users");
                db.AdminUsers.AddRange(adminUsers);
                db.SaveChanges();
            }
        }
    }
}
