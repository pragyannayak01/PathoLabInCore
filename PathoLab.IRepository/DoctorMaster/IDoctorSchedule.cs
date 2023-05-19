using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PathoLab.Domain.DoctorMaster;

namespace PathoLab.IRepository.DoctorMaster
{
   public interface IDoctorSchedule
    {
        Task<List<DoctorSchedule>> GetOne(int UserId);

    }
}
