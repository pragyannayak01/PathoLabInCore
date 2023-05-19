using PathoLab.Domain.MenuMaster;
using PathoLab.Domain.SubMenuMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.SubMenuMaster
{
     public interface ISubMenuRepository
    {
        Task<List<SubMenuClass>> SubMenuSelectAll(SubMenuClass submenuclass);
        Task<SubMenuClass> SubMenuSelectOne(int SubMenuId);
        Task<int> SubMenuInsertAndUpdate(SubMenuClass entity);
        Task<int> SubMenuDelete(int SubMenuId);
        Task<List<MenuClass>> GetAllMenu();
    }
}
