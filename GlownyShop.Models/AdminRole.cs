using System;
using System.Collections.Generic;
using System.Text;

namespace GlownyShop.Models
{
    public class AdminRole : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AdminUserRole> AdminUserRoles { get; set; }
    }
}
