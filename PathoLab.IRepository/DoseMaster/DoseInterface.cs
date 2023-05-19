using PathoLab.Domain.DoseMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.DoseMaster
{
   public interface DoseInterface
    {
        Task<List<Dose>> GetAll(Dose dignosis);
        Task<List<Dose>> BindMedicine();
        //Task<List<Dose>> BindDignosisCatagoryName(Dose dignosis);
        Task<Dose> GetById(int DignosisID);
        Task<Dose> GetSubCatagoryNameByMedicineId(int MedicineId);
        Task<int> Create(Dose entity);
        Task<int> Delete(int DignosisID);
        Task<List<Dose>> DosesGetByCatId(int MedicineId);
    }
}
