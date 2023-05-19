using Dapper;
using PathoLab.Domain.Account;
using PathoLab.Domain.DignosisConfigurationMaster;
using PathoLab.Domain.HospitalMaster;
using PathoLab.Domain.LabTestMaster;
using PathoLab.Domain.PathoBilMasterl;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.PathoBillMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.PathoBillMaster
{
   public  class PathoBillRepository:RepositoryBase, IPathoBillRepository
    {
        public PathoBillRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<List<LabTest>> AutoFillTestName()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "AutoFillTestName");
                var x = Connection.Query<LabTest>("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PathoBill> BindCollection()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "BindCollection");
                var x = Connection.Query<PathoBill>("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PathoBill> BindCurrentDate()
        
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "BindDate");
                var x = Connection.Query<PathoBill>("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PathoBill>> BindingTest(int PathoBillId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PathoBillId", PathoBillId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "BindingTest");
                var x = Connection.Query<PathoBill>("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure).AsList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<List<DignosisConfiguration>> BindingTestConfig(int PathoBillId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@PathoBillId", PathoBillId);
                param.Add("@action", "BindingTestConfig");
                var x = Connection.Query<DignosisConfiguration>("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async  Task<int> DeletePathoBill(int PathoBillId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PathoBillId", PathoBillId);
                param.Add("@action", "DeletePathoBill");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<int> DeleteToUpdateReportCollection(int PathoBillId, int LabTestId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PathoBillId", PathoBillId);
                param.Add("@LabTestId", LabTestId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "DeleteToUpdateReportCollection");
                Connection.Execute("[USP_PL_PathoBill]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async  Task<List<User>> GetAllDoctor()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "GetAllDoctor");
                var x = Connection.Query<User>("USP_PL_DDL", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async  Task<List<PathoBill>> GetAllPathoBill(PathoBill pathobill)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetAllPathoBill");
                var x = Connection.Query<PathoBill>("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PathoBill>> GetAllPathoBillRecord(PathoBill pathobill)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetAllPathoBillRecord");
                var x = Connection.Query<PathoBill>("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<HospitalEntity> GetHospitalDetails(int HospitalID)
        {
            try
            {

                DynamicParameters param = new DynamicParameters();
                param.Add("@HospitalID", HospitalID);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetHospitalDetails");
                var x = Connection.Query<HospitalEntity>("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //By using mobile no get patient details 
        public async Task<PathoBill> GetPatientByMobile(string Mobile)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Mobile", Mobile);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetPatientByMobile");
                var x = Connection.Query<PathoBill>("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PathoBill> GetPatientDetailsByPathoBillId(int PathoBillId, int LabTestId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PathoBillId", PathoBillId);
                param.Add("@LabTestId", LabTestId);
                param.Add("@action", "GetPatientDetailsByPathoBillId");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<PathoBill>("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return doc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<LabTest> GetPriceByTestId(int LabTestId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "AutoFillPrice");
                param.Add("@LabTestId", LabTestId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<LabTest>("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> InsertingTestFields(LabTests entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PathoBillId", entity.PathoBillId);
                param.Add("@LabTestId", entity.LabTestId); 
                param.Add("@LabTestName", entity.LabTestName); 
                param.Add("@Price", entity.Price);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "InsertingTestFields");
                Connection.Execute("USP_PL_PathoBill", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<int> InsertPathoBill(PathoBill entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@CollectionId", entity.CollectionId);
                param.Add("@Barcode", entity.Barcode);
                //param.Add("@LabTestId", entity.LabTestId);
                param.Add("@Mobile", entity.Mobile);
                param.Add("@FullName", entity.FullName);
                param.Add("@Age", entity.Age);
                param.Add("@Gender", entity.Gender);
                param.Add("@Email", entity.  Email);
                param.Add("@DoctorName", entity.DoctorName);
               // param.Add("@DateOfAppointment", entity.DateOfAppointment);
                param.Add("@PayMode", entity.PayMode);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "InsertPathoBill");
                Connection.Execute("[USP_PL_PathoBill]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<int> InsertPathoReportCollection(PathoTestValue entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LabTestId", entity.LabTestId);
                param.Add("@PathoBillId", entity.PathoBillId);
                param.Add("@DignosisConfigId", entity.DignosisConfigId);
                param.Add("@Value", entity.Value);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "InsertPathoReportCollection");
                Connection.Execute("[USP_PL_PathoBill]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> CreateInfo(PathoBill entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SampleColNo", entity.SampleColNo);
                param.Add("@PathoBillId", entity.PathoBillId);
                param.Add("@CollectionId", entity.CollectionId);
                param.Add("@action", "InsertSampleCollectionInfo");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("[Usp_SampleCollection]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<int> CreateSample(Sample entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SampleColNo", entity.SampleColNo);
                param.Add("@PathoBillId", entity.PathoBillId);
                //param.Add("@CollectionId", entity.CollectionId);
                param.Add("@LabTestId", entity.LabTestId);
                param.Add("@Barcode", entity.Barcode);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "CollectionInformation");
                Connection.Execute("[Usp_SampleCollection]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
