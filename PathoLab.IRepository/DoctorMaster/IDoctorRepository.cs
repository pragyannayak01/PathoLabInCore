using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PathoLab.Domain.DoctorMaster;

namespace PathoLab.IRepository.DoctorMaster
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAll(Doctor doctor);
        Task<Doctor> GetOne(int DoctorID);
        Task<int> Create(Doctor entity);
        Task<int> Delete(int DoctorID);
    }
}
