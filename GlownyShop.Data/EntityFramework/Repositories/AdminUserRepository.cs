using GlownyShop.Core.Data;
using GlownyShop.Models;
using Microsoft.Extensions.Logging;

namespace GlownyShop.Data.EntityFramework.Repositories
{
    public class AdminUserRepository : BaseRepository<AdminUser, int>, IAdminUserRepository
    {
        public AdminUserRepository()
        {
        }

        public AdminUserRepository(GlownyShopContext db, ILogger<AdminUserRepository> logger)
            : base(db, logger)
        {
        }
    }
}