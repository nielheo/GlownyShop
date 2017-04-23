using GlownyShop.Models;
using System;
using System.Threading.Tasks;

namespace GlownyShop.Core.Data
{
    public interface IAdminUserRepository : IBaseRepository<AdminUser, string>
    {
        Task<AdminUser> GetByEmail(string email);
    }
}