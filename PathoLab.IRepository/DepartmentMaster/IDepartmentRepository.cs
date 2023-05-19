using PathoLab.Domain.DepartmentMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.DepartmentMaster
{
   public  interface IDepartmentRepository
    {
        Task<List<DepartmentName>> GetAll(DepartmentName departmentname);
        Task<DepartmentName> GetOne(int DepartmentId);
        Task<int> Create(DepartmentName entity);
        Task<int> Delete(int DepartmentId);
        Task<List<DepartmentName>> DepartmentDDL();
    }
}
