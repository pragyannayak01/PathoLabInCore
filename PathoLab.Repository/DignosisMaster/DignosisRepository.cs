using Dapper;
using PathoLab.Domain.DignosisMaster;
using PathoLab.IRepository.DignosisMaster;
using PathoLab.IRepository.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.DignosisMaster
{
     public class DignosisRepository:RepositoryBase, IDignosisRepository
    {
        public DignosisRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<int> Create(Dignosis entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DignosisID", entity.DignosisID); 
                param.Add("@Name", entity.Name);
                param.Add("@DignosisCatagoryID", entity.DignosisCatagoryID);
             
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                if (entity.DignosisID == 0)
                {
                    param.Add("@action", "Insert");
                }
                else
                {
                    param.Add("@action", "Update");
                }
                var query = "USP_PL_DignosisMaster";
                Connection.Execute(query, param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<int> Delete(int DignosisID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DignosisID", DignosisID);
                param.Add("@action", "Delete");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("USP_PL_DignosisMaster", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<List<Dignosis>> GetAll(Dignosis dignosis)
        
        
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "SelectAll");
                if (dignosis.Name == null)
                    dignosis.Name = "";
                param.Add("@Name", dignosis.Name);
                param.Add("@DignosisCatagoryID", dignosis.DignosisCatagoryID);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var dign = Connection.Query<Dignosis>("USP_PL_DignosisMaster", param, commandType: CommandType.StoredProcedure).ToList();
                return dign;
            }


            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Dignosis>> BindDignosisCatagoryName(Dignosis dignosis)


        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "BindDignosisCatagoryName");
              
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var dign = Connection.Query<Dignosis>("USP_PL_DignosisMaster", param, commandType: CommandType.StoredProcedure).ToList();
                return dign;
            }


            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Dignosis> GetOne(int DignosisID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@DignosisID", DignosisID);
                param.Add("@action", "SelectOne");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var x = Connection.Query<Dignosis>("USP_PL_DignosisMaster", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
