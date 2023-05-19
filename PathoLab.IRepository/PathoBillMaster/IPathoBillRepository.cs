using PathoLab.Domain.Account;
using PathoLab.Domain.DignosisConfigurationMaster;
using PathoLab.Domain.HospitalMaster;
using PathoLab.Domain.LabTestMaster;
using PathoLab.Domain.PathoBilMasterl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.PathoBillMaster
{
     public interface IPathoBillRepository
    {
        Task<int> InsertPathoBill(PathoBill entity);
        Task<List<PathoBill>> GetAllPathoBill(PathoBill pathobill);
        Task<List<PathoBill>> GetAllPathoBillRecord(PathoBill pathobill);
        Task<PathoBill> GetPatientByMobile(string Mobile );
        Task<List<User>> GetAllDoctor();
        Task<List<LabTest>> AutoFillTestName();
        Task<LabTest> GetPriceByTestId(int LabTestId);
        Task<int> InsertingTestFields(LabTests entity);
        Task<PathoBill> GetPatientDetailsByPathoBillId(int PathoBillId,int LabTestId); 
        Task<List<DignosisConfiguration>> BindingTestConfig(int PathoBillId);
        Task<List<PathoBill>> BindingTest(int PathoBillId);
        Task<PathoBill> BindCollection();
        Task<PathoBill> BindCurrentDate();
        Task<int> DeletePathoBill(int PathoBillId);
        Task<int> InsertPathoReportCollection(PathoTestValue entity);
        Task<int> DeleteToUpdateReportCollection(int PathoBillId, int LabTestId);//Bind DignosisConfiguration At the Time Of Edit
        Task<HospitalEntity> GetHospitalDetails(int HospitalID);
        Task<int> CreateSample(Sample entity);
        Task<int> CreateInfo(PathoBill entity);


    }
}
