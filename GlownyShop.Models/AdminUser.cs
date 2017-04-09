using System;
using System.Collections.Generic;
using System.Text;

namespace GlownyShop.Models
{
    public class AdminUser : IEntity<int>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public ICollection<AdminUserRole> AdminUserRoles { get; set; }
    }
}
