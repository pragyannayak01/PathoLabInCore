using Dapper;
using PathoLab.Domain.DesignationMaster;
using PathoLab.Domain.PermissionMaster;
using PathoLab.Domain.SubMenuMaster;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.PermissionMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.PermissionMaster
{
     public class PermissionRepository:RepositoryBase, IPermissionRepository
    {
        public PermissionRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<List<DesignationName>> DesignationDDL()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "GetAllDesg");
                var doc = Connection.Query<DesignationName>("USP_PL_DDL", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> PermissionInsert(Permission entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SubMenuId", entity.SubMenuId);
                param.Add("@DesignationId", entity.DesignationId);
                param.Add("@UserId", entity.UserId);
                param.Add("@IsChecked", entity.IsChecked);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "PermissionInsert");               
                Connection.Execute("USP_PL_PermissionTable", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> PermissionUpdateToDelete(int DesignationId, int UserId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DesignationId", DesignationId);
                param.Add("@UserId", UserId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "PermissionUpdateToDelete");
                Connection.Execute("USP_PL_PermissionTable", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<SubMenuClass>> GetSelectedSubMenus(int DesignationId, int UserId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DesignationId", DesignationId);
                param.Add("@UserId", UserId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetSelectedSubMenus");
                var x = Connection.Query<SubMenuClass>("USP_PL_PermissionTable", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SubMenuClass>> GetSelectedMenuByDesig(int DesignationId, int UserId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DesignationId", DesignationId);
                param.Add("@UserId", UserId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetSelectedMenuByDesig");
                var x = Connection.Query<SubMenuClass>("USP_PL_PermissionTable", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SubMenuClass>> GetSelectedSubMenuByDesig(int DesignationId, int UserId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DesignationId", DesignationId);
                param.Add("@UserId", UserId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetSelectedSubMenuByDesig");
                var x = Connection.Query<SubMenuClass>("USP_PL_PermissionTable", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
