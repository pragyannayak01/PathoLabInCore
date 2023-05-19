using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PathoLab.Domain.HospitalMaster;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.HospitalMaster;


namespace PathoLab.Repository.HospitalMaster
{
    public class HospitalRepository : RepositoryBase, IHospitalRepository
    {
        public HospitalRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
        public async Task<int> Create(HospitalEntity entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@HospitalID", entity.HospitalID);
                param.Add("@HospitalName", entity.HospitalName);
                param.Add("@RegstrationNo", entity.RegstrationNo);
                param.Add("@LandlineNo", entity.LandlineNo);
                param.Add("@Address", entity.Address);
                param.Add("@City", entity.City);
                param.Add("@State", entity.State);
                param.Add("@PinCode", entity.PinCode);
                param.Add("@ContactPerson", entity.ContactPerson);
                param.Add("@MobielNo", entity.MobielNo);
                param.Add("@GSTNo", entity.GSTNo);
                param.Add("@HEmail", entity.HEmail);
                param.Add("@HWebsite", entity.HWebsite);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                if (entity.HospitalID == 0)
                {
                    param.Add("@action", "I");
                }
                else
                {
                    param.Add("@action", "U");
                }
                Connection.Execute("USP_PL_Hospital", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;


            }
            catch (Exception ex)
            {

                
                throw ex;
            }

        }

        public async Task<int> Delete(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@HospitalID", id);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "D");

                Connection.Execute("USP_PL_Hospital", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public async Task<List<HospitalEntity>> GetAll(HospitalEntity hosp)
        {
            try
            {

                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "SelectAll");
                param.Add("@HospitalName", hosp.HospitalName);
                //param.Add("@RegstrationNo", hosp.RegstrationNo);
                //param.Add("@LandlineNo", hosp.LandlineNo);
                //param.Add("@Address", hosp.Address);
                param.Add("@City", hosp.City);
                param.Add("@State", hosp.State);
                //param.Add("@PinCode", hosp.PinCode);
                //param.Add("@ContactPerson", hosp.ContactPerson);
                //param.Add("@MobielNo", hosp.MobielNo);
                //param.Add("@GSTNo", hosp.GSTNo);

                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                var doc = Connection.Query<HospitalEntity>("USP_PL_Hospital", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;
            }
            catch (Exception ex)
            {

                
                throw ex;
            }
        }

        public async Task<HospitalEntity> GetOne(int HospitalID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@HospitalID", HospitalID);
                param.Add("@action", "SelectOne");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<HospitalEntity>("USP_PL_Hospital", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return doc;


            }
            catch (Exception ex)
            {

                
                throw ex;
            }

        }
        public async Task<List<HospitalEntity>> HospitalDDL()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "GetAllHospital");
                var doc = Connection.Query<HospitalEntity>("USP_PL_DDL", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
