using Dapper;
using PathoLab.Domain.MenuMaster;
using PathoLab.Domain.SubMenuMaster;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.SubMenuMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.SubMenuMaster
{
     public class SubMenuRepository : RepositoryBase, ISubMenuRepository
    {
        public SubMenuRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public  async Task<int> SubMenuDelete(int SubMenuId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SubMenuId", SubMenuId);
                param.Add("@action", "SubMenuDelete");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("USP_PL_SubMenuTable", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<MenuClass>> GetAllMenu()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "GetAllMenu");
                var x = Connection.Query<MenuClass>("USP_PL_DDL", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SubMenuInsertAndUpdate(SubMenuClass entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SubMenuId", entity.SubMenuId);
                param.Add("@SubMenuName", entity.SubMenuName);
                param.Add("@SubMenuURL", entity.SubMenuURL);
                param.Add("@MenuId", entity.MenuId);
                param.Add("@SlNo", entity.SlNo);
                param.Add("@SubMenuDescription", entity.SubMenuDescription);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                if (entity.SubMenuId == 0)
                {
                    param.Add("@action", "SubMenuInsert");
                }
                else
                {
                    param.Add("@action", "SubMenuUpdate");
                }
                Connection.Execute("USP_PL_SubMenuTable", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SubMenuClass>> SubMenuSelectAll(SubMenuClass submenuclass)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "SubMenuSelectAll");
                var x = Connection.Query<SubMenuClass>("USP_PL_SubMenuTable", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SubMenuClass> SubMenuSelectOne(int SubMenuId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SubMenuId", SubMenuId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "SubMenuSelectOne");
                var x = Connection.Query<SubMenuClass>("USP_PL_SubMenuTable", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
