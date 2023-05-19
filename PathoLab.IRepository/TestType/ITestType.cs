using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PathoLab.Domain.TestType;

namespace PathoLab.IRepository.TestType
{
   public interface ITestType
    {
        Task<List<TestTypeMaster>> GetAll(TestTypeMaster TestTypeID);
        Task<TestTypeMaster> GetOne(int TestTypeID);
        Task<int> Create(TestTypeMaster entity);
        //Task<int> Update(ClientMaster entity);
        Task<int> Delete(int TestTypeID);
    }
}
