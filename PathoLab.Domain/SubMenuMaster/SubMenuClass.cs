using System;
using System.Collections.Generic;
using System.Text;

namespace PathoLab.Domain.SubMenuMaster
{
    public class SubMenuClass
    {
        //SubMenuId,SubMenuName,MenuNameURL,MenuId,SlNo,SubMenuDescription
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string IconName { get; set; }
        public string SubMenuURL { get; set; }
        public int SlNo { get; set; }
        public string SubMenuDescription { get; set; }
        public int UserId { get; set; }
        public int DesignationId { get; set; }
        public bool IsChecked { get; set; }
    }
}
