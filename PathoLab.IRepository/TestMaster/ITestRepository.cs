using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PathoLab.Domain.TestMaster;
using PathoLab.Domain.TestType;

namespace PathoLab.IRepository.TestMaster
{
    public interface ITestRepository
    {
        Task<List<Test>> GetAll(Test elmnt);
        Task<Test> GetOne(int TestID);
        Task<int> Create(Test entity);
        
        Task<int> Delete(int TestID);
        Task<List<TestTypeMaster>> Dropdown();

    }
}
