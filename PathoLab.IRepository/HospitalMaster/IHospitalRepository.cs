using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PathoLab.Domain.HospitalMaster;

namespace PathoLab.IRepository.HospitalMaster
{
   public interface IHospitalRepository
    {
        Task<List<HospitalEntity>> GetAll(HospitalEntity hosp);
        Task<HospitalEntity> GetOne(int HospitalID);
        Task<int> Create(HospitalEntity entity);
        //Task<int> Update(ClientMaster entity);
        Task<int> Delete(int id);
        Task<List<HospitalEntity>> HospitalDDL();
    }
}
