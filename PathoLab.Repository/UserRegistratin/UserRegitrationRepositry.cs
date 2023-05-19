using Dapper;
using PathoLab.Domain.Account;
using PathoLab.Domain.RoleMaster;
using PathoLab.Domain.UserRegistration;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.UserRegistration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.UserRegistration
{
    public class UserRegitrationRepositry : RepositoryBase, IUser
    {
        public UserRegitrationRepositry(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<int> delete(int p)
        {
            try
            {


                DynamicParameters param = new DynamicParameters();

                param.Add("@UserId", p);

                param.Add("@mode", "D");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                Connection.Execute("[USP_MPL_USER]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));

                return x;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<UserModel>> GetAllUser(UserModel us)
        {
            try
            {
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@mode", "A");
                ObjParm.Add("@FullName", us.FullName);
                ObjParm.Add("@DesignationId ", us.DesignationId);
                var query = "USP_MPL_USER";
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                var GetAppById = Connection.Query<UserModel>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();

                return GetAppById;

            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public async Task<UserModel> GetbyidUser(int id)
        {
            try
            {

                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@UserId", id);
                ObjParm.Add("@mode", "S");
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                var query = "USP_MPL_USER";
                var GetAppById = Connection.Query<UserModel>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById[0];




            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public async Task<int> insert(UserModel om)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", om.UserId);
                param.Add("@UserName", om.UserName);
                param.Add("@Password", om.Password);
                param.Add("@FullName", om.FullName);
                param.Add("@Email", om.Email);
                param.Add("@Mobile", om.Mobile);
                param.Add("@Gender", om.Gender);
                param.Add("@Address", om.Address);////////////
                param.Add("@DOB", om.DOB);
                param.Add("@City", om.City);
                param.Add("@DesignationId", om.DesignationId);
                param.Add("@@DepartmentId", om.DepartmentId);
                param.Add("@HospitalID", om.HospitalID);////////////
                param.Add("@Address1", om.Address1);
                param.Add("@mode", "IU");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("[USP_MPL_USER]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<Role>> BindRole()
        {
            try
            {
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@mode", "Bind");
                var query = "USP_MPL_USER";
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                var GetAppById = Connection.Query<Role>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();

                return GetAppById;

            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public async Task<List<UserModel>> DoctorDDL()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "GetAllDoctor");
                var doc = Connection.Query<UserModel>("USP_PL_DDL", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
