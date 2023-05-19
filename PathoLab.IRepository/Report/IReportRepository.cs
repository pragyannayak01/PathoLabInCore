using PathoLab.Domain.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.Report
{
     public interface IReportRepository
    {
        Task<List<ReportEntity>> DailyDateWiseAppointment(ReportEntity reportentity);
        Task<List<ReportEntity>> HospitalWiseAppointment(ReportEntity reportentity);
    }
}
