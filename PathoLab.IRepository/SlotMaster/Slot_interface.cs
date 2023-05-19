using PathoLab.Domain.SloteMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.SlotMaster
{
    public interface Slot_interface
    {
        Task<int> insert(SlotEntity p);
        Task<int> delete(int p);
        Task<List<SlotEntity>> GetAllSlot(SlotEntity p);
        Task<SlotEntity> GetbyidSlot(int id);
        Task<List<SlotEntity>> SlotHIdDDL(int HospitalID);
    }
}
