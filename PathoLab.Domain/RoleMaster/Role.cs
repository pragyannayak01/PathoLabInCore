using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PathoLab.Domain.RoleMaster
{
   public class Role
    {
        [Key]
        public int RoleId { get; set; } = 0;
        [Required]
        public string RoleName { get; set; } = null;
    }
}
