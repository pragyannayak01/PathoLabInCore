using Dapper;
using PathoLab.Domain.TestType;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.TestType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.TestType
{
   public class TestTypeRepository:RepositoryBase,ITestType
    {
        public TestTypeRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<int> Create(TestTypeMaster entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TestTypeID", entity.TestTypeID);
                param.Add("@TestType", entity.TestType);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                if (entity.TestTypeID == 0)
                {
                    param.Add("@action", "I");
                }
                else
                {
                    param.Add("@action", "U");
                }
                int x = Connection.Execute("USP_PL_TestType", param, commandType: CommandType.StoredProcedure);
                return x;

            }
            catch (Exception ex)
            {

                return 0;
                throw ex;
            }


        }

        public async Task<int> Delete(int TestTypeID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TestTypeID", TestTypeID);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "D");
                int x = Connection.Execute("USP_PL_TestType", param, commandType: CommandType.StoredProcedure);
                return x;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<TestTypeMaster>> GetAll(TestTypeMaster TestTypeID)
        {
            try
            {

                DynamicParameters param = new DynamicParameters();
                param.Add("@TestTypeID", TestTypeID.TestTypeID);
                param.Add("@TestType", TestTypeID.TestType);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "SelectAll");
                var doc = Connection.Query<TestTypeMaster>("USP_PL_TestType", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;
            }
            catch (Exception ex)
            {

                
                throw ex;
            }

        }

        public async Task<TestTypeMaster> GetOne(int TestTypeID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TestTypeID", TestTypeID);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "SelectOne");
                var doc = Connection.Query<TestTypeMaster>("USP_PL_TestType", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return doc;

            }
            catch (Exception ex)
            {

            
                throw ex;
            }

        }
    }
}
