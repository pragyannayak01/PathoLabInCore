using Dapper;
using PathoLab.Domain.PatientAppointmentMaster;
using PathoLab.Domain.SlotMappig;
using PathoLab.Domain.UserRegistration;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.PatientAppointmentMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PathoLab.Repository.PatientAppointmentMaster
{
    public class PatientAppointmentRepository:RepositoryBase, IPatientAppointmentRepository
    {
        public PatientAppointmentRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public  async Task<int> Create(PatientAppointment entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DateOfAppointment", entity.DateOfAppointment);
                param.Add("@RegdNo", entity.RegdNo);
                param.Add("@HospitalID", entity.HospitalID);
                param.Add("@DepartmentId", entity.DepartmentId);
                param.Add("@DoctorId", entity.DoctorId);
                param.Add("@SlotID", entity.SlotID);
                param.Add("@PatientID", entity.PatientID);
                param.Add("@PatientName", entity.PatientName);
                param.Add("@MobileNo", entity.MobileNo);
                param.Add("@Email", entity.Email);
                param.Add("@Age", entity.Age);
                param.Add("@Gender", entity.Gender);
                param.Add("@City", entity.City);
                param.Add("@Address", entity.Address);
                param.Add("@Password", entity.Password);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                if (entity.AppointmentId == 0)
                {
                    param.Add("@action", "Insert");
                }
                else
                {
                    param.Add("@action", "InsertAndUpdate");
                }
                var query = "TSP_PL_PatientAppointment";
                Connection.Execute(query, param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<List<PatientAppointment>> GetAll(PatientAppointment patientAppointment)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "SelectAll");
                //param.Add("@DateOfAppointment", patientAppointment.DateOfAppointment);
                //param.Add("@HospitalID", patientAppointment.HospitalID);
                //param.Add("@DepartmentId", patientAppointment.DepartmentId);
                //param.Add("@DoctorId", patientAppointment.DoctorId);
                //param.Add("@SlotID ", patientAppointment.SlotID);
                //param.Add("@PatientID", patientAppointment.PatientID);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<PatientAppointment>("TSP_PL_PatientAppointment", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<List<SlotMapping>> GetSlotByHostIdAndDoctIdAndDOA(int HospitalID, int DoctorId, DateTime DateOfAppointment)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "SlotByHostnDocId");
                param.Add("@HospitalID", HospitalID);
                param.Add("@DoctorId", DoctorId);
                param.Add("@DateOfAppointment", DateOfAppointment);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<SlotMapping>("TSP_PL_PatientAppointment", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserModel>> GetDoctorByDepartmentId(int DepartmentId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "DoctorByDeptId");
                param.Add("@DepartmentId", DepartmentId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<UserModel>("TSP_PL_PatientAppointment", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PatientAppointment> GetOne(int AppointmentId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "SelectOne");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var x = Connection.Query<PatientAppointment>("TSP_PL_PatientAppointment", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<List<SlotMapping>> GetAvlCaptBySIdNDOA(int SlotID, DateTime DateOfAppointment)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "AvlCaptBySIdNDOA");
                param.Add("@SlotID", SlotID);
                param.Add("@DateOfAppointment", DateOfAppointment);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<SlotMapping>("TSP_PL_PatientAppointment", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<int> DeleteAppointment(int AppointmentId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AppointmentId", AppointmentId);
                param.Add("@action", "Delete");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("TSP_PL_PatientAppointment", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserModel>> DoctorNameByHAndDId(int HospitalID, int DepartmentId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@HospitalID", HospitalID);
                param.Add("@DepartmentId", DepartmentId);
                param.Add("@action", "DoctorNameByHAndDId");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<UserModel>("TSP_PL_PatientAppointment", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
