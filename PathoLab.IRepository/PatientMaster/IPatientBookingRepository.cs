using PathoLab.Domain.PatientMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.PatientMaster
{
     public interface IPatientBookingRepository
    {
        Task<List<PatientBooking>> PatientBookingDetails(int UserId);

    }
}
