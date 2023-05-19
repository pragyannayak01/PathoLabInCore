using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PathoLab.Domain.LabTestMaster;
using PathoLab.Domain.DignosisMaster;
namespace PathoLab.IRepository.LabTestMaster
{
   public interface ILabtest
    {
        Task<int> insert(LabTest p);

        Task<int> delete(LabTest p);

        Task<List<LabTest>> GetAllLabTest(LabTest p);

        Task<LabTest> GetTestbyid(int id);
        public Task<List<Dignosis>> GetAllDignosis();
    }
}
