namespace GlownyShop.Core.Logic
{
    public interface ISecurityService
    {
        bool ValidateAdminUser(string email, string password);
    }
}