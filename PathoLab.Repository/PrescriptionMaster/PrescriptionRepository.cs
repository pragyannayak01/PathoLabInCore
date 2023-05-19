using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PathoLab.Domain.DignosisMaster;
using PathoLab.Domain.MedicineMaster;
using PathoLab.Domain.PrescriptionMaster;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.PrescriptionMaster;

namespace PathoLab.Repository.PrescriptionMaster
{
    public class PrescriptionRepository : RepositoryBase, IPrescriptionRepository
    {
        public PrescriptionRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
        //Create Prescription
        public async Task<int> CreatePrescription(Prescription entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PatientID", entity.PatientID);
                param.Add("@PrescriptionId", entity.PrescriptionId);
                param.Add("@HospitalID", entity.HospitalID);
                param.Add("@GuardianName", entity.GuardianName);
                param.Add("@Height", entity.Height);               
                param.Add("@BloodPressure", entity.BloodPressure);
                param.Add("@Weight", entity.Weight);
                param.Add("@Pulse", entity.Pulse);
                param.Add("@Temperature", entity.Temperature);
                param.Add("@OxygenLevel", entity.OxygenLevel);
                param.Add("@Symptoms", entity.Symptoms);
                param.Add("@PatientHistory", entity.PatientHistory);
                param.Add("@DignosisID", entity.DignosisID);
                //param.Add("@Name", entity.Name);
                //param.Add("@MorningAfterMeal", entity.MorningAfterMeal);
                //param.Add("@MorningBeforeMeal", entity.MorningBeforeMeal);
                //param.Add("@AfternoonAfterMeal", entity.AfternoonAfterMeal);
                //param.Add("@AfternoonBeforeMeal", entity.AfternoonBeforeMeal);
                //param.Add("@NightAfterMeal", entity.NightAfterMeal);
                //param.Add("@NightBeforeMeal", entity.NightBeforeMeal);
                param.Add("@OtherAdvice", entity.OtherAdvice);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                if (entity.PrescriptionId == 0)
                {
                    param.Add("@action", "Insert");
                }
                else
                {
                    param.Add("@action", "Update");
                }
                Connection.Execute("[Usp_Prescrption]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        // Selected Dignosis Bind During Edit
        public async Task<int> DeleteToUpdatePresDig(int PrescriptionId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PrescriptionId", PrescriptionId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "DeleteToUpdatePresDig");
                Connection.Execute("[Usp_Prescrption]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Selected Medicine Bind During Edit
        public async Task<int> DeleteToUpdatePresMed(int PrescriptionId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PrescriptionId", PrescriptionId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "DeleteToUpdatePresMed");
                Connection.Execute("[Usp_Prescrption]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Create Prescribe Dignoisis
        public async Task<int> CreatePresDignosis(Prescription entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PrescriptionId", entity.PrescriptionId);
                param.Add("@DignosisID", entity.DignosisID);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "InsertPresDignosis");
                Connection.Execute("[Usp_Prescrption]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Create Prescribe Medicine
        public async Task<int> CreatePresMedicine(Medicinee entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PrescriptionId", entity.PrescriptionId);
                param.Add("@Id", entity.Id);
                //param.Add("@MedicineName", entity.MedicineName);
                //param.Add("@MorningAfterMeal", entity.MorningAfterMeal);
                //param.Add("@MorningBeforeMeal", entity.MorningBeforeMeal);
                //param.Add("@AfternoonAfterMeal", entity.AfternoonAfterMeal);
                //param.Add("@AfternoonBeforeMeal", entity.AfternoonBeforeMeal);
                //param.Add("@NightAfterMeal", entity.NightAfterMeal);
                //param.Add("@NightBeforeMeal", entity.NightBeforeMeal);
                param.Add("@DoseId", entity.DoseId);
                param.Add("@Duration", entity.Duration);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "InsertPresMedicine");
                Connection.Execute("[Usp_Prescrption]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Delete Prescription In View Page
        public async Task<int> Delete(int PrescriptionId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PrescriptionId", PrescriptionId);
                param.Add("@action", "DeletePrescription");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("Usp_Prescrption", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //All Prescription Details
        public async Task<List<Prescription>> GetAllPrescriptionDetails(Prescription prescription)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetAllPrescriptionDetails");
                var x = Connection.Query<Prescription>("Usp_Prescrption", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Get All Selected Dignosis
        public async Task<List<Dignosis>> GetAllSelectedDignosis(int PrescriptionId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PrescriptionId", PrescriptionId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetAllSelectedDignosis");
                var x = Connection.Query<Dignosis>("Usp_Prescrption", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        //Get All Dignosis Name IN DropDowm List
        public async Task<List<Dignosis>> GetDignosis()
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
        //Get All Medicine Name IN AutoFill 
        public async Task<List<Medicine>> GetMedicineName()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "AutoFillMedicine");
                var x = Connection.Query<Medicine>("USP_PL_DDL", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //DataBind:-Get Patient Details By PatientId
        public async Task<List<Prescription>> GetPatientDetailsByPatientID(int UserId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", UserId);
                param.Add("@action", "BindPatientDetails");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<Prescription>("Usp_Prescrption", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //DataBind:-Get Prescription Details By PatientId and PrescriptionId
        public async Task<Prescription> GetPrescriptionDetailsByPId(int PrescriptionId, int PatientID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PrescriptionId", PrescriptionId);
                param.Add("@PatientID", PatientID);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetPrescriptionDetailsByPId");
                var x = Connection.Query<Prescription>("Usp_Prescrption", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Print Prescription Details By PatientId and PrescriptionId
        public async Task<Prescription> PrintPrescriptionDetailsByPId(int PrescriptionId, int PatientID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PrescriptionId", PatientID);
                param.Add("@PatientID", PrescriptionId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "PrintPrescriptionDetailsByPId");
                var x = Connection.Query<Prescription>("Usp_Prescrption", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get All Selected Medicine
        public async Task<List<Medicinee>> GetAllSelectedMedicine(int PrescriptionId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PrescriptionId", PrescriptionId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "GetAllSelectedMedicine");
                var x = Connection.Query<Medicinee>("Usp_Prescrption", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Prescription> GetHospitalDetails(int HospitalID)
        {
            try
            {

                DynamicParameters param = new DynamicParameters();
                param.Add("@HospitalID", HospitalID);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "HospitalAddressbind");
                var x = Connection.Query<Prescription>("Usp_Prescrption", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
