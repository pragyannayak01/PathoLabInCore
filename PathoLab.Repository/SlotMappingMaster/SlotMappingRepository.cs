using Dapper;
using PathoLab.Domain.SlotMappig;
using PathoLab.Domain.UserRegistration;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.SlotMappingMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.SlotMappingMaster
{
    public class SlotMappingRepository : RepositoryBase, ISlotMappingRepository
    {
        public SlotMappingRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<int> Create(SlotMapping entity)
        {

            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@HospitalID", entity.HospitalID);
                param.Add("@SlotID", entity.SlotID);
                param.Add("@DoctorId", entity.DoctorId);
                param.Add("@DaysId", entity.DaysId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "Insert");
                var query = "USP_PL_SlotMapping";
                Connection.Execute(query, param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public async Task<int> DeleteToUpdate(int SlotID, int DoctorId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SlotID", SlotID);
                param.Add("@DoctorId", DoctorId);
                param.Add("@action", "DeleteToUpdate");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("USP_PL_SlotMapping", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> Delete(int SMId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SMId", SMId);
                param.Add("@action", "Delete");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("USP_PL_SlotMapping", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SlotMapping>> GetAll(SlotMapping slotMapping)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "SelectAll");
                param.Add("@HospitalID", slotMapping.HospitalID);
                param.Add("@SlotID ", slotMapping.SlotID);
                param.Add("@DoctorId", slotMapping.DoctorId);
                param.Add("@DaysId", slotMapping.DaysId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<SlotMapping>("USP_PL_SlotMapping", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SlotMapping>> GetAllShiftBySAndDId(int SlotID, int DoctorId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SlotID", SlotID);
                param.Add("@DoctorId", DoctorId);
                param.Add("@action", "SelectOne");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var x = Connection.Query<SlotMapping>("USP_PL_SlotMapping", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SlotMapping> SlotTimeByHnSId(int HospitalID, int SlotID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@HospitalID", HospitalID);
                param.Add("@SlotID", SlotID);
                param.Add("@action", "SlotTimeByHnSId");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var x = Connection.Query<SlotMapping>("USP_PL_SlotMapping", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<UserModel>> DoctorNameByHId(int HospitalID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@HospitalID", HospitalID);
                param.Add("@action", "DoctorNameByHId");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var x = Connection.Query<UserModel>("USP_PL_SlotMapping", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

