using Dapper;
using PathoLab.Domain.Report;
using PathoLab.IRepository.Factory;
using PathoLab.IRepository.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.Repository.Report
{
     public class ReportRepository:RepositoryBase, IReportRepository
    {
        public ReportRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<List<ReportEntity>> DailyDateWiseAppointment(ReportEntity reportentity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@HospitalID", reportentity.HospitalID);
                param.Add("@action", "DailyDateWiseAppointment");
                var x = Connection.Query<ReportEntity>("USP_PL_Report", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<List<ReportEntity>> HospitalWiseAppointment(ReportEntity reportentity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@HospitalID", reportentity.HospitalID);
                param.Add("@FromDate", reportentity.FromDate);
                param.Add("@ToDate", reportentity.ToDate);
                param.Add("@action", "HospitalWiseAppointment");
                var x = Connection.Query<ReportEntity>("USP_PL_Report", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
