using PathoLab.Domain.RoleMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.RoleMaster
{
   public interface IRole
    {
        Task<int> insert(Role p);

        Task<int> delete(int p);

        Task<List<Role>> GetAllRole();

        Task<Role> RoleGetbyid(int id);
    }
}
