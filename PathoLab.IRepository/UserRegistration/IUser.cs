using PathoLab.Domain.Account;
using PathoLab.Domain.RoleMaster;
using PathoLab.Domain.UserRegistration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.UserRegistration
{
   public interface IUser
    {
        Task<int> insert(UserModel p);
       
        Task<int> delete(int p);

        Task<List<UserModel>> GetAllUser(UserModel us);

        Task<UserModel> GetbyidUser(int id);
        Task<List<Role>> BindRole();

        Task<List<UserModel>> DoctorDDL();
    }
}
