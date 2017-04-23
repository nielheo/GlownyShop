using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GlownyShop.Core.Data;
using GlownyShop.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace GlownyShop.Data.EntityFramework.Repositories
{
    public class AdminUserRepository : BaseRepository<AdminUser, string>, IAdminUserRepository
    {
        public AdminUserRepository()
        {
        }

        public AdminUserRepository(GlownyShopContext db, ILogger<AdminUserRepository> logger)
             : base(db, logger)
        {
        }

        public Task<AdminUser> GetByEmail(string email)
        {
            _logger.LogInformation("Get AdminUser with email = {email}", email);
            return _db.Set<AdminUser>().SingleOrDefaultAsync(c => c.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}