using GlownyShop.Models;
using System.Collections.Generic;

namespace GlownyShop.Core.Logic
{
    public interface ISecurityService
    {
        AdminUser ValidateAdminUser(string email, string password);
        IEnumerable<AdminRole> GetAdminRoles(string adminUserId);
    }
}