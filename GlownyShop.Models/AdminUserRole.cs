namespace GlownyShop.Models
{
    public class AdminUserRole
    {
        public int AdminUserId { get; set; }
        public AdminUser AdminUser { get; set; }

        public int AdminRoleId { get; set; }
        public AdminRole AdminRole { get; set; }
    }
}