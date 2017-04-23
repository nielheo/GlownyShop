using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlownyShop.Models
{
    public class AdminUser : IEntity<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [MaxLength(200)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(200)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(200)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(200)]
        [Required]
        [DataType(DataType.Password)]
        [DefaultValue("")]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public ICollection<AdminUserRole> AdminUserRoles { get; set; }
    }
}