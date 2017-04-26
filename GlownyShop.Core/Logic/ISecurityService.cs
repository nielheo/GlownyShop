using GlownyShop.Models;

namespace GlownyShop.Core.Logic
{
    public interface ISecurityService
    {
        AdminUser ValidateAdminUser(string email, string password);
    }
}