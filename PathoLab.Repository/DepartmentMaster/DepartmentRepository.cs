using Dapper;
using PathoLab.Domain.DepartmentMaster;
using PathoLab.IRepository.DepartmentMaster;
using PathoLab.IRepository.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.DepartmentMaster
{
    public class DepartmentRepository:RepositoryBase,IDepartmentRepository
    {
        public DepartmentRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<int> Create(DepartmentName entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DepartmentId", entity.DepartmentId);
                param.Add("@Department", entity.Department);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                if (entity.DepartmentId == 0)
                {
                    param.Add("@action", "DepartmentInsert");
                }
                else
                {
                    param.Add("@action", "DepartmentUpdate");
                }
                var query = "USP_PL_DepartmentMaster";
                Connection.Execute(query, param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(int DepartmentId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DepartmentId", DepartmentId);
                param.Add("@action", "DepartmentDelete");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("USP_PL_DepartmentMaster", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DepartmentName>> GetAll(DepartmentName departmentname)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Department", departmentname.Department);
                param.Add("@action", "DepartmentSelectAll");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<DepartmentName>("USP_PL_DepartmentMaster", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DepartmentName> GetOne(int DepartmentId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DepartmentId", DepartmentId);
                param.Add("@action", "DepartmentSelectOneById");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var x = Connection.Query<DepartmentName>("USP_PL_DepartmentMaster", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DepartmentName>> DepartmentDDL()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "GetAllDept");
                var doc = Connection.Query<DepartmentName>("USP_PL_DDL", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
