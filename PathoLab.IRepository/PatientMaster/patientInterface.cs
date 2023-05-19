using PathoLab.Domain.PatientMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.PatientMaster
{
   public interface patientInterface
    {
        Task<int> insert(patient p);

        Task<int> delete(int p);

        Task<List<patient>> GetAllPatient(patient p);

        Task<patient> Getbyidpatient(int id);
    }
}
