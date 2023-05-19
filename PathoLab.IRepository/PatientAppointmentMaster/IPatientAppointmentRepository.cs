using PathoLab.Domain.PatientAppointmentMaster;
using PathoLab.Domain.SlotMappig;
using PathoLab.Domain.UserRegistration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.PatientAppointmentMaster
{
    public interface IPatientAppointmentRepository
    {
        Task<List<PatientAppointment>> GetAll(PatientAppointment patientAppointment);
        Task<List<SlotMapping>> GetSlotByHostIdAndDoctIdAndDOA(int HospitalID, int DoctorId, DateTime DateOfAppointment);
        Task<List<SlotMapping>> GetAvlCaptBySIdNDOA( int SlotID, DateTime DateOfAppointment);
        Task<int> Create(PatientAppointment entity);
        Task<PatientAppointment> GetOne(int AppointmentId);
        Task<List<UserModel>> GetDoctorByDepartmentId(int DepartmentId);
        Task<int> DeleteAppointment(int AppointmentId);
        Task<List<UserModel>> DoctorNameByHAndDId(int HospitalID, int DepartmentId);

    }
}
