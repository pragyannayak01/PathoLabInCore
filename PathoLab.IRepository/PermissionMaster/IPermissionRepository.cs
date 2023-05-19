using PathoLab.Domain.DesignationMaster;
using PathoLab.Domain.PermissionMaster;
using PathoLab.Domain.SubMenuMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.PermissionMaster
{
     public interface IPermissionRepository
    {
        Task<int> PermissionInsert(Permission entity);
        Task<int> PermissionUpdateToDelete(int DesignationId, int UserId);
        Task<List<DesignationName>> DesignationDDL();
        Task<List<SubMenuClass>> GetSelectedSubMenus(int DesignationId, int UserId); 
        Task<List<SubMenuClass>> GetSelectedMenuByDesig(int DesignationId, int UserId);
        Task<List<SubMenuClass>> GetSelectedSubMenuByDesig(int DesignationId, int UserId);

    }
}
