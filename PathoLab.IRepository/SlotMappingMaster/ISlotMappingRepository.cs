using PathoLab.Domain.HospitalMaster;
using PathoLab.Domain.SlotMappig;
using PathoLab.Domain.UserRegistration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.SlotMappingMaster
{
    public interface ISlotMappingRepository
    {
        Task<List<SlotMapping>> GetAll(SlotMapping slotMapping);
        Task<List<SlotMapping>> GetAllShiftBySAndDId(int SlotID,int DoctorId);
        Task<int> Create(SlotMapping entity);
        Task<int> Delete(int SMId);
        Task<SlotMapping> SlotTimeByHnSId(int HospitalID, int SlotID);
        Task<List<UserModel>> DoctorNameByHId(int HospitalID);
        Task<int> DeleteToUpdate(int SlotID, int DoctorId); 
    }
}
