using Dapper;
using PathoLab.Domain.RoleMaster;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.RoleMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.RoleMaster
{
   public class RoleRepository:RepositoryBase, IRole
    {
        public RoleRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
        public async Task<List<Role>> GetAllRole()
        {
            try
            {
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@mode", "A");
                var query = "USP_PL_RoleMaster";
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                var GetAppById = Connection.Query<Role>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById;


            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public async Task<Role> RoleGetbyid(int RoleId)
        {
            try
            {

                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@RoleId", RoleId);
                ObjParm.Add("@mode", "S");
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                var query = "USP_PL_RoleMaster";
                var GetAppById = Connection.Query<Role>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById[0];



            }
            catch (Exception ex)
            {
                return null;

            }
        }


        public async Task<int> insert(Role om)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@RoleName", om.RoleName);
                param.Add("@RoleId", om.RoleId);

                param.Add("@mode", "IU");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                Connection.Execute("[USP_PL_RoleMaster]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));

                return x;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> delete(int RoleId)
        {
            try
            {


                DynamicParameters param = new DynamicParameters();

                param.Add("@RoleId", RoleId);

                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                param.Add("@mode", "D");
                Connection.Execute("[USP_PL_RoleMaster]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));

                return x;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}