using PathoLab.Domain.DignosisConfigurationMaster;
using PathoLab.Domain.DignosisMaster;
using PathoLab.Domain.LabTestMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static PathoLab.Domain.DignosisConfigurationMaster.DignosisConfiguration;

namespace PathoLab.IRepository.DignosisConfigurationMaster
{
   public  interface IDignosisConfigurationRepository
    {
        Task<int> InsertUpdateDignosisConfiguration(DignosisConfiguration entity);
        Task<List<DignosisConfiguration>> GetDignosisConfigurationById(int DignosisID,int LabTestId);
        Task<List<Dignosis>> GetAllDignosis();
        Task<List<LabTest>> GetAllLabTest();
        Task<List<DignosisConfiguration>> GetAllDignosisConfiguration(DignosisConfiguration dignosisconfiguration);
        Task<int> DeleteDignosisConfiguration(int DignosisID, int LabTestId);
        Task<int> DeleteToUpdateDignosisConfiguration(int DignosisID, int LabTestId);//Bind DignosisConfiguration At the Time Of Edit




    }
}
