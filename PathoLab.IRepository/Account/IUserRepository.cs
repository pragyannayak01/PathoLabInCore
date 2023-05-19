using PathoLab.Domain.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.Account
{
    public interface IUserRepository
    {
         Task<User> UserGetByUserNamePwd(string UserName, string Password);
        Task<int> UpdatePassword(User ue);
    }
}
