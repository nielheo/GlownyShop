using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlownyShop.Models
{
    public class AdminUser : IEntity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Email { get; set; }

        [MaxLength(200)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(200)]
        [Required]
        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public ICollection<AdminUserRole> AdminUserRoles { get; set; }
    }
}