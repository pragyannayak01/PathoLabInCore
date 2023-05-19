using System;
using System.Collections.Generic;
using System.Text;
using PathoLab.IRepository.DoctorMaster;
using PathoLab.IRepository.Factory;
using PathoLab.Domain.DoctorMaster;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Linq;

namespace PathoLab.Repository.DoctorMaster
{
    public class DoctorScheduleRepository: RepositoryBase,IDoctorSchedule
    {
        public DoctorScheduleRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
        public async Task<List<DoctorSchedule>> GetOne(int UserId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", UserId);
                param.Add("@action", "DoctorSchedule");
                //param.Add("@PMSGOUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var doc = Connection.Query<DoctorSchedule>("USP_PL_DoctorDashboard", param, commandType: CommandType.StoredProcedure).ToList();
                return doc;


            }
            catch (Exception ex)
            {


                throw ex;
            }

        }
    }
}
