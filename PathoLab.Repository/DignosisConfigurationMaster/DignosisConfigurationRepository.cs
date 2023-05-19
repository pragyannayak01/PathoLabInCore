using Dapper;
using PathoLab.Domain.DignosisConfigurationMaster;
using PathoLab.Domain.DignosisMaster;
using PathoLab.Domain.LabTestMaster;
using PathoLab.IRepository.DignosisConfigurationMaster;
using PathoLab.IRepository.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PathoLab.Domain.DignosisConfigurationMaster.DignosisConfiguration;

namespace PathoLab.Repository.DignosisConfigurationMaster
{
    public class DignosisConfigurationRepository : RepositoryBase, IDignosisConfigurationRepository
    {
        public DignosisConfigurationRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }


        public async Task<int> DeleteDignosisConfiguration(int DignosisID, int LabTestId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LabTestId", LabTestId);
                param.Add("@DignosisID", DignosisID);
                param.Add("@action", "DeleteDignosisConfiguration");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("USP_PL_DignosisConfiguration", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteToUpdateDignosisConfiguration(int DignosisID, int LabTestId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LabTestId", LabTestId);
                param.Add("@DignosisID", DignosisID);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "DeleteToUpdateDignosisConfiguration");
                Connection.Execute("[USP_PL_DignosisConfiguration]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public async Task<List<DignosisConfiguration>> GetAllDignosisConfiguration(DignosisConfiguration dignosisconfiguration)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetAllDignosisConfiguration");
                var x = Connection.Query<DignosisConfiguration>("USP_PL_DignosisConfiguration", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LabTest>> GetAllLabTest()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "GetAllLabTest");
                var x = Connection.Query<LabTest>("USP_PL_DDL", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DignosisConfiguration>> GetDignosisConfigurationById(int DignosisID, int LabTestId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DignosisID", DignosisID);
                param.Add("@LabTestId", LabTestId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetDignosisConfigurationById");
                var x = Connection.Query<DignosisConfiguration>("USP_PL_DignosisConfiguration", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> InsertUpdateDignosisConfiguration(DignosisConfiguration entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DignosisConfigId", entity.DignosisConfigId);
                param.Add("@LabTestId", entity.LabTestId);
                param.Add("@DignosisID", entity.DignosisID);
                param.Add("@InvestigationName", entity.InvestigationName);
                param.Add("@MinimumPercentage", entity.MinimumPercentage);
                param.Add("@MaximumPercentage", entity.MaximumPercentage);
                param.Add("@Unit", entity.Unit);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "Insert");
                Connection.Execute("[USP_PL_DignosisConfiguration]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
