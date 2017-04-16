using GlownyShop.Core.Data;
using GlownyShop.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlownyShop.Data.EntityFramework.Repositories
{
    public class AdminUserRepository : BaseRepository<AdminUser, int>, IAdminUserRepository
    {
        public AdminUserRepository() { }

        public AdminUserRepository(GlownyShopContext db, ILogger<AdminUserRepository> logger)
            : base(db, logger)
        {
        }
    }
}
