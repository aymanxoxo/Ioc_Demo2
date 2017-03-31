using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(50, ErrorMessageResourceType = typeof(Resources.ServerLabel), ErrorMessageResourceName = "NameMaxLength")]
        public string FullName { get; set; }

        [Key]
        [Required(ErrorMessageResourceType = typeof(Resources.ServerLabel), ErrorMessageResourceName = "EmailRequired")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Resources.ServerLabel), ErrorMessageResourceName = "EmailMaxLength")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.ServerLabel), ErrorMessageResourceName = "EmailFormat")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ServerLabel), ErrorMessageResourceName = "PasswordRequired")]
        [MinLength(6, ErrorMessageResourceType = typeof(Resources.ServerLabel), ErrorMessageResourceName = "PasswordMinLength")]
        [MaxLength(12, ErrorMessageResourceType = typeof(Resources.ServerLabel), ErrorMessageResourceName = "PasswordMaxLength")]
        public string Password { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
