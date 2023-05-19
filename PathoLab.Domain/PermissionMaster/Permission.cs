using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.PermissionMaster
{
    public class Permission
    {
        // PermissionId,SubMenuId,DesignationId,SubMenuDescription
        public int PermissionId { get; set; }
        public int SubMenuId { get; set; }
        public string MenuName { get; set; }
        public string SubMenuName { get; set; }
        public int DesignationId { get; set; }
        public int UserId { get; set; }
        public string SubMenuDescription { get; set; }
        public bool IsChecked { get; set; }
    }
}
