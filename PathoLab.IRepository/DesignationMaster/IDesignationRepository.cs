using PathoLab.Domain.DesignationMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.DegisnationMaster
{
    public interface IDesignationRepository
    {
        Task<List<DesignationName>> GetAll(DesignationName Designationname);
        Task<DesignationName> GetOne(int DesignationId);
        Task<int> Create(DesignationName entity);
        Task<int> Delete(int DesignationId);
        Task<List<DesignationName>> DesignationDDL();
    }
}
