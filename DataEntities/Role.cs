using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntities
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ServerLabel), ErrorMessageResourceName = "RoleNameRequired")]
        [MaxLength(20, ErrorMessageResourceType = typeof(Resources.ServerLabel), ErrorMessageResourceName = "RoleNameMaxLength")]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
