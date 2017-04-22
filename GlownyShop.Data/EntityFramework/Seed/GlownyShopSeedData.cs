using GlownyShop.Core.Logic;
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

            // admin users
            var AdminUserSuperAdmin = new AdminUser
            {
                //Id = 1,
                FirstName = "Super",
                LastName = "Admin",
                Email = "superadmin@glowny-shop.com",
                Password = SecurityService.GenerateHashedPassword("P@ssw0rd"),
                IsActive = true,
            };

            var AdminUserUserAdmin = new AdminUser
            {
                //Id = 2,
                FirstName = "User",
                LastName = "Admin",
                Email = "useradmin@glowny-shop.com",
                Password = SecurityService.GenerateHashedPassword("P@ssw0rd"),
                IsActive = true,
            };

            var AdminUserClientAdmin1 = new AdminUser
            {
                //Id = 3,
                FirstName = "Client",
                LastName = "Admin 1",
                Email = "clientadmin1@glowny-shop.com",
                Password = SecurityService.GenerateHashedPassword("P@ssw0rd"),
                IsActive = true,
            };

            var AdminUserClientAdmin2 = new AdminUser
            {
                //Id = 4,
                FirstName = "Client",
                LastName = "Admin 2",
                Email = "clientadmin2@glowny-shop.com",
                Password = SecurityService.GenerateHashedPassword("P@ssw0rd"),
                IsActive = true,
            };

            var adminUsers = new List<AdminUser>
            {
                AdminUserSuperAdmin,
                AdminUserUserAdmin,
                AdminUserClientAdmin1,
                AdminUserClientAdmin2,
            };
            if (!db.AdminUsers.Any())
            {
                db._logger.LogInformation("Seeding admin users");
                db.AdminUsers.AddRange(adminUsers);
                db.SaveChanges();
            }

            // admin Roles
            var AdminRoleSuperAdmin = new AdminRole
            {
                Id = 0,
                Name = "Super Admin",
                AdminUserRoles = new List<AdminUserRole>
                {
                    new AdminUserRole { AdminUser = AdminUserSuperAdmin }
                }
            };
            var AdminRoleUserAdmin = new AdminRole
            {
                Id = 1,
                Name = "User Admin",
                AdminUserRoles = new List<AdminUserRole>
                {
                    new AdminUserRole { AdminUser = AdminUserUserAdmin },
                    new AdminUserRole { AdminUser = AdminUserClientAdmin2 }
                }
            };
            var AdminRoleClientAdmin = new AdminRole
            {
                Id = 2,
                Name = "Client Admin",
                AdminUserRoles = new List<AdminUserRole>
                {
                    new AdminUserRole { AdminUser = AdminUserClientAdmin1 },
                    new AdminUserRole { AdminUser = AdminUserClientAdmin2 }
                }
            };

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
        }
    }
}