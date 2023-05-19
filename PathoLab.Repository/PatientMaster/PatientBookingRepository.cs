using Dapper;
using PathoLab.Domain.PatientMaster;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.PatientMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.PatientMaster
{
     public class PatientBookingRepository:RepositoryBase, IPatientBookingRepository
    {
        public PatientBookingRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<List<PatientBooking>> PatientBookingDetails(int UserId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", UserId);
                param.Add("@action", "PatientBooking");
               // param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var paient = Connection.Query<PatientBooking>("[USP_PL_PatientDashboard]", param, commandType: CommandType.StoredProcedure).ToList();
                return paient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
