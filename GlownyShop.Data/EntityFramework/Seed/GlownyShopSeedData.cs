using GlownyShop.Core.Logic;
using GlownyShop.Models;
using Microsoft.Extensions.Logging;
using System;
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
                Id = new Guid("1d9a394c-60b8-4523-ab41-52b2936836c3").ToString(),
                FirstName = "Super",
                LastName = "Admin",
                Email = "superadmin@glowny-shop.com",
                Password = SecurityService.GenerateHashedPassword("P@ssw0rd"),
                IsActive = true,
            };

            var AdminUserUserAdmin = new AdminUser
            {
                Id = new Guid("5a736c82-2c9b-4030-abd5-52058549920c").ToString(),
                FirstName = "User",
                LastName = "Admin",
                Email = "useradmin@glowny-shop.com",
                Password = SecurityService.GenerateHashedPassword("P@ssw0rd"),
                IsActive = true,
            };

            var AdminUserClientAdmin1 = new AdminUser
            {
                Id = new Guid("15220f30-c001-47ca-a539-58dab2371b79").ToString(),
                FirstName = "Client",
                LastName = "Admin 1",
                Email = "clientadmin1@glowny-shop.com",
                Password = SecurityService.GenerateHashedPassword("P@ssw0rd"),
                IsActive = true,
            };

            var AdminUserClientAdmin2 = new AdminUser
            {
                Id = new Guid("67a4ac55-f101-411d-90fa-709fcac666b1").ToString(),
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