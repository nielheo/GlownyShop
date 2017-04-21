using GlownyShop.Core.Data;
using GlownyShop.Models;
using Microsoft.Extensions.Logging;

namespace GlownyShop.Data.EntityFramework.Repositories
{
    public class AdminRoleRepository : BaseRepository<AdminRole, int>, IAdminRoleRepository
    {
        public AdminRoleRepository()
        {
        }

        public AdminRoleRepository(GlownyShopContext db, ILogger<AdminRoleRepository> logger)
            : base(db, logger)
        {
        }
    }
}