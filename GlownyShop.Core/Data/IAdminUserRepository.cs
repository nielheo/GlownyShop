using GlownyShop.Models;
using System.Threading.Tasks;

namespace GlownyShop.Core.Data
{
    public interface IAdminUserRepository : IBaseRepository<AdminUser, int>
    {
        Task<AdminUser> GetByEmail(string email);
    }
}