using GlownyShop.Models;
using System.Threading.Tasks;

namespace GlownyShop.Core.Logic
{
    public interface IAdminUserRoleService
    {
        Task<AdminUser> AddAdminUser(AdminUser adminUser);
        Task<AdminUser> UpdateAdminUser(AdminUser adminUser);
    }
}