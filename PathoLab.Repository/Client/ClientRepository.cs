using PathoLab.IRepository.Client;
using PathoLab.IRepository.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.SqlServer;
using System.Threading.Tasks;
using PathoLab.Domain.Client;
using System.Data;
using System.Linq;

namespace PathoLab.Repository.Client
{
   public class ClientRepository:RepositoryBase,IClientRepository
    {
        public ClientRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public  async Task<int> Create(ClientMaster entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ClintID", entity.ClintID);
                param.Add("@Name", entity.Name);
                param.Add("@Address", entity.Address);
                param.Add("@City ", entity.City);
                param.Add("@phoneno", entity.phoneno);
                param.Add("@WhatsAppNo", entity.WhatsAppNo);
                param.Add("@ReferByClientId ", entity.ReferByClientId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                //param.Add("@EDate", entity.EDate);
                //param.Add("@Status", entity.Status);
                
                if (entity.ClintID == 0)
                {
                    param.Add("@action", "I");
                }
                else
                {
                    param.Add("@action", "U");
                }
                 Connection.Execute("[USP_PL_ClientMast]", param, commandType: CommandType.StoredProcedure);
                int x  = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
               

            }
            catch (Exception ex)
            {

                //return 0;
                throw ex;
            }
            
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ClintID", id);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@action", "D");

                 Connection.Execute("USP_PL_ClientMast", param, commandType: CommandType.StoredProcedure);
                int x = Convert.ToInt32(param.Get<string>("@PMSGOUT"));
                return x;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            
        }

        public async Task<List<ClientMaster>> GetAll(ClientMaster client)
        {
            try
            {

                DynamicParameters param = new DynamicParameters();
                param.Add("@action", "SelectAll");
                param.Add("@Name", client.Name);
                param.Add("@Address", client.Address);
                param.Add("@City ", client.City);
                param.Add("@phoneno", client.phoneno);
                param.Add("@WhatsAppNo", client.WhatsAppNo);
                param.Add("@ReferByClientId ", client.ReferByClientId);
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                var doc =Connection.Query<ClientMaster>("USP_PL_ClientMast", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;
            }
            catch (Exception ex)
            {

                //return null;
                throw ex;
            }
        }

      public async  Task<ClientMaster> GetOne(int clientid)
        { 
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ClintID", clientid);
                param.Add("@action", "SelectOne");
                param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<ClientMaster>("USP_PL_ClientMast", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return  doc;

            }
            catch (Exception ex)
            {

               //return null ;
                throw ex;
            }
            
        }

        //public async Task<int> Update(ClientMaster entity)
        //{
        //    try
        //    {
        //        DynamicParameters param = new DynamicParameters();
        //        param.Add("@")
        //        param.Add("@Name", entity.Name);
        //        param.Add("@Address", entity.Address);
        //        param.Add("@City", entity.City);
        //        param.Add("@phoneno ", entity.phoneno);
        //        param.Add("@WhatsAppNo", entity.WhatsAppNo);
        //        param.Add("@ReferByClientId", entity.ReferByClientId);
        //        param.Add("@EDate ", entity.EDate);
        //        param.Add("@Status", entity.Status);
        //        param.Add("@action", "U");
        //        int x = Connection.Execute("SP_ClientMast1", param, commandType: CommandType.StoredProcedure);
        //        Connection.Close();
        //        return x;
        //    }
        //    catch (Exception ex)
        //    {

        //        return 0;
        //    }


        }
    }


