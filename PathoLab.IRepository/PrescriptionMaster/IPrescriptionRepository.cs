using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PathoLab.Domain.DignosisMaster;
using PathoLab.Domain.MedicineMaster;
using PathoLab.Domain.PrescriptionMaster;
namespace PathoLab.IRepository.PrescriptionMaster
{
    public interface IPrescriptionRepository
    {
        Task<int> CreatePrescription(Prescription entity);
        Task<List<Prescription>> GetPatientDetailsByPatientID(int UserId);
        Task<List<Dignosis>> GetDignosis();
        Task<List<Medicine>>GetMedicineName();
        Task<int> CreatePresDignosis(Prescription entity);
        Task<int> DeleteToUpdatePresDig(int PrescriptionId);//Bind Dignosis At the Time Of Edit
        Task<int> CreatePresMedicine(Medicinee entity);
        Task<int> DeleteToUpdatePresMed(int PrescriptionId);//Bind Medicine At the Time Of Edit
        Task<List<Prescription>> GetAllPrescriptionDetails(Prescription prescription);
        Task<Prescription> GetPrescriptionDetailsByPId( int PrescriptionId, int PatientID );
        Task<Prescription> PrintPrescriptionDetailsByPId(int PrescriptionId, int PatientID);
        Task<int> Delete(int PrescriptionId);
        Task<List<Dignosis>> GetAllSelectedDignosis(int PrescriptionId);
        Task<List<Medicinee>> GetAllSelectedMedicine(int PrescriptionId);
        Task<Prescription> GetHospitalDetails(int HospitalID);


    }
}
