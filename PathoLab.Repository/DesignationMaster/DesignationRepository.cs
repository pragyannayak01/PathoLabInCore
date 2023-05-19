using Dapper;
using PathoLab.Domain.DesignationMaster;
using PathoLab.IRepository.DegisnationMaster;
using PathoLab.IRepository.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.DesignationMaster
{
    public class DesignationRepository:RepositoryBase, IDesignationRepository
    {
        public DesignationRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<int> Create(DesignationName entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DesignationId", entity.DesignationId);
                param.Add("@Designation", entity.Designation);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                if (entity.DesignationId == 0)
                {
                    param.Add("@action", "DesignationInsert");
                }
                else
                {
                    param.Add("@action", "DesignationUpdate");
                }
                var query = "USP_PL_DesignationMaster";
                Connection.Execute(query, param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async  Task<int> Delete(int DesignationId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DesignationId", DesignationId);
                param.Add("@action", "DesignationDelete");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("USP_PL_DesignationMaster", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DesignationName>> GetAll(DesignationName Designationname)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "DesignationSelectAll");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<DesignationName>("USP_PL_DesignationMaster", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async  Task<DesignationName> GetOne(int DesignationId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DesignationId", DesignationId);
                param.Add("@action", "DesignationSelectOneById");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var x = Connection.Query<DesignationName>("USP_PL_DesignationMaster", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
    }
}
