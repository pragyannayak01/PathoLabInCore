using Dapper;
using PathoLab.Domain.Account;
using PathoLab.IRepository.Account;
using PathoLab.IRepository.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PathoLab.Repository.Account
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<int> UpdatePassword(User ue)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
               
                param.Add("@UserName", ue.UserName);
                param.Add("@Password", ue.Password);
                param.Add("@action", "changepassword");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                string spName = "USP_PL_USER_LOGIN_MANAGE";
                Connection.Execute(spName, param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return  result;


            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<User> UserGetByUserNamePwd(string UserName, string Password)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "LoginPage");
                param.Add("@UserName", UserName);
                param.Add("@Password", Password);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                string spName = "USP_PL_USER_LOGIN_MANAGE"; 
                var user = await Connection.QueryAsync<User>(spName, param, commandType: CommandType.StoredProcedure);
                return user.FirstOrDefault() ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
