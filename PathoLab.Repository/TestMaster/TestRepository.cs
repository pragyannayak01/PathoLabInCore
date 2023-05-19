using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PathoLab.Domain.TestMaster;
using PathoLab.Domain.TestType;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.TestMaster;

namespace PathoLab.Repository.TestMaster
{
    public class TestRepository:RepositoryBase,ITestRepository
    {
        public TestRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<int> Create(Test entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TestID", entity.TestID);
                param.Add("@TestName", entity.TestName);
                param.Add("@TestType", entity.TestType);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                

                if (entity.TestID == 0)
                {
                    param.Add("@action", "I");
                }
                else
                {
                    param.Add("@action", "U");
                }
                Connection.Execute("[USP_PL_TestMaster]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;


            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<int> Delete(int TestID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TestID", TestID);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "D");

                Connection.Execute("USP_PL_TestMaster", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<TestTypeMaster>> Dropdown()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                //param.Add("@TestTypeID", elmnt.TestTypeID);
                //param.Add("@TestType", elmnt.TestType);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "dropdown");
                var doc = Connection.Query<TestTypeMaster>("USP_PL_TestMaster", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Test>> GetAll(Test elmnt)
        {

            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TestID", elmnt.TestID);
                param.Add("@TestType", elmnt.TestType);
                param.Add("@TestName", elmnt.TestName);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "SelectAll");
                var doc = Connection.Query<Test>("USP_PL_TestMaster", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<Test> GetOne(int TestID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TestID", TestID);
                param.Add("@action", "SelectOne");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<Test>("USP_PL_TestMaster", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return doc;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
