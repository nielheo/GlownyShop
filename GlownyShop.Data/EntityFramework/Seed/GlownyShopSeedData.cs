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

            // episodes
            var AdminRoleSuperAdmin = new AdminRole { Id = 0, Name = "Super Admin" };
            var AdminRoleUserAdmin = new AdminRole { Id = 1, Name = "User Admin" };
            var adminRoles = new List<AdminRole>
            {
                AdminRoleSuperAdmin,
                AdminRoleUserAdmin,
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
