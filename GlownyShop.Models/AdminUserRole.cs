using System;

namespace GlownyShop.Models
{
    public class AdminUserRole
    {
        public string AdminUserId { get; set; }
        public AdminUser AdminUser { get; set; }

        public int AdminRoleId { get; set; }
        public AdminRole AdminRole { get; set; }
    }
}