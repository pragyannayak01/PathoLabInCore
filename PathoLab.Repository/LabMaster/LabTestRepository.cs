using PathoLab.IRepository.Factory;
using PathoLab.IRepository.LabTestMaster;
using System;
using System.Collections.Generic;
using System.Text;
using PathoLab.Domain.LabTestMaster;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using PathoLab.Domain.DignosisMaster;

namespace PathoLab.Repository.LabMaster
{
    public class LabTestRepository:RepositoryBase,ILabtest
    {
        public LabTestRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<List<Dignosis>> GetAllDignosis()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "GetAllDignosis");
                var x = Connection.Query<Dignosis>("USP_PL_DDL", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> delete(LabTest p)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@LabTestId", p.LabTestId);

                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                param.Add("@action", "D");
                Connection.Execute("[Usp_LabTest]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));

                return x;

            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public async Task<List<LabTest>> GetAllLabTest(LabTest p)
        {
            try
            {
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@action", "SelectAll");
                var query = "Usp_LabTest";
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                var GetAppById = Connection.Query<LabTest>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById;


            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public async Task<LabTest> GetTestbyid(int id)
        {
            try
            {

                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@LabTestId", id);
                ObjParm.Add("@action", "SelectOne");
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "Usp_LabTest";
                var GetAppById = Connection.Query<LabTest>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById[0];



            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public async Task<int> insert(LabTest p)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LabTestId", p.LabTestId);
                param.Add("@LabTestName", p.LabTestName);
                param.Add("@Price", p.Price);
                param.Add("@DignosisID", p.DignosisID);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                if (p.LabTestId == 0)
                {
                    param.Add("@action", "I");
                }
                else
                {
                    param.Add("@action", "U");
                }

                var query = "Usp_LabTest";

                Connection.Execute(query, param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
