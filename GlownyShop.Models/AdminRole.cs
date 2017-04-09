using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GlownyShop.Models
{
    public class AdminRole : IEntity<int>
    {
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        public ICollection<AdminUserRole> AdminUserRoles { get; set; }
    }
}