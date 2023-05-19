using Dapper;
using PathoLab.Domain.DoseMaster;
using PathoLab.IRepository.DoseMaster;
using PathoLab.IRepository.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.DoseMaster
{
    public class DoseRepository : RepositoryBase, DoseInterface
    {
        public DoseRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
        
        public async Task<List<Dose>> BindMedicine()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "Bind_Medicine");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var dign = Connection.Query<Dose>("Usp_DoseMaster", param, commandType: CommandType.StoredProcedure).AsList();
                return dign;
            }


            catch (Exception ex)
            {
                return null;
            }
        }
        

        public async Task<int> Create(Dose entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", entity.UserId);
                param.Add("@DoseId", entity.DoseId);
                param.Add("@DoseName", entity.DoseName);
                //param.Add("@MedicineId", entity.MedicineId);
                param.Add("@CatagoryId", entity.CatagoryId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                param.Add("@action", "InsertDose");

                var query = "Usp_DoseMaster";
                Connection.Execute(query, param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(int DignosisID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DoseID", DignosisID);
                param.Add("@action", "D");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("Usp_DoseMaster", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Dose>> GetAll(Dose d)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "A");
                //param.Add("@MedicineId",d.id);
                param.Add("@CatagoryId", d.CatagoryId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var dign = Connection.Query<Dose>("Usp_DoseMaster", param, commandType: CommandType.StoredProcedure).AsList();
                return dign;
            }


            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Dose> GetById(int DignosisID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DoseID", DignosisID);
                param.Add("@action", "S");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var x = Connection.Query<Dose>("Usp_DoseMaster", param, commandType: CommandType.StoredProcedure).AsList();
                return x[0];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    

        public async Task<Dose> GetSubCatagoryNameByMedicineId(int MedicineId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@MedicineId", MedicineId);

                param.Add("@action", "GetSubCatagoryNameByMedicineId");

                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var dign = Connection.Query<Dose>("Usp_DoseMaster", param, commandType: CommandType.StoredProcedure).AsList();
                return dign[0];
            }


            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<Dose>> DosesGetByCatId(int MedicineId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "Bind_DosesByMed");
                param.Add("@MedicineId", MedicineId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var x = Connection.Query<Dose>("Usp_DoseMaster", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }


            catch (Exception ex)
            {
                return null;
            }
        }
    }

    
}
