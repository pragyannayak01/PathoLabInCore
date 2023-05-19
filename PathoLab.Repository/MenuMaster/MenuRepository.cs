using Dapper;
using PathoLab.Domain.MenuMaster;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.MenuMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading. Tasks;

namespace PathoLab.Repository.MenuMaster
{
     public class MenuRepository:RepositoryBase, IMenuRepository
    {
        public MenuRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<int> MenuDelete(int MenuId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@MenuId", MenuId);
                param.Add("@action", "MenuDelete");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("USP_PL_MenuTable", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> MenuInsertAndUpdate(MenuClass entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@MenuId", entity.MenuId);
                param.Add("@MenuName", entity.MenuName);
                param.Add("@SlNo", entity.SlNo);
                param.Add("@IconName", entity.IconName);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                if (entity.MenuId == 0)
                {
                    param.Add("@action", "MenuInsert");
                }
                else
                {
                    param.Add("@action", "MenuUpdate");
                }
                Connection.Execute("USP_PL_MenuTable", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MenuClass>> MenuSelectAll(MenuClass menuclass)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "MenuSelectAll");
                var x = Connection.Query<MenuClass>("USP_PL_MenuTable", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MenuClass> MenuSelectOne(int MenuId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@MenuId", MenuId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "MenuSelectOne");
                var x = Connection.Query<MenuClass>("USP_PL_MenuTable", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
