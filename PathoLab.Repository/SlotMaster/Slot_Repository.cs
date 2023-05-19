using Dapper;
using PathoLab.Domain.SloteMaster;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.SlotMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.SlotMaster
{
    public class Slot_Repository : RepositoryBase, Slot_interface
    {
        public Slot_Repository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<int> delete(int p)
        {
            try
            {


                DynamicParameters param = new DynamicParameters();

                param.Add("@SlotID", p);

                param.Add("@mode", "D");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                Connection.Execute("[USP_Slot_Master]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));

                return x;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }



        public async Task<List<SlotEntity>> GetAllSlot(SlotEntity p)
        {
            try
            {
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@mode", "A");
                ObjParm.Add("@HospitalID",p.HospitalID);
                var query = "USP_Slot_Master";
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                var GetAppById = Connection.Query<SlotEntity>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();

                return GetAppById;

            }
            catch (Exception ex)
            {
                return null;

            }
        }



        public async Task<SlotEntity> GetbyidSlot(int id)
        {
            try
            {

                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@SlotID", id);
                ObjParm.Add("@mode", "S");
                ObjParm.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);


                var query = "USP_Slot_Master";
                var GetAppById = Connection.Query<SlotEntity>(query, ObjParm, commandType: CommandType.StoredProcedure).AsList();
                return GetAppById[0];




            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public async Task<int> insert(SlotEntity om)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SlotID", om.SlotID);
                param.Add("@SlotName", om.SlotName);
                param.Add("@HospitalID", om.HospitalID);
                param.Add("@Slot_TimeFrom", om.Slot_TimeFrom);
                param.Add("@Slot_TimeTo", om.Slot_TimeTo);
                param.Add("@mode", "IU");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("[USP_Slot_Master]", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<List<SlotEntity>> SlotHIdDDL(int HospitalID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "GetAllSlotByHospital");
                param.Add("@HospitalID", HospitalID);
                var doc = Connection.Query<SlotEntity>("USP_PL_DDL", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
