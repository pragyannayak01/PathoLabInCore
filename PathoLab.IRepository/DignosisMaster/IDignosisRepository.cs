using PathoLab.Domain.DignosisMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PathoLab.IRepository.DignosisMaster
{
     public interface IDignosisRepository
    {
        Task<List<Dignosis>> GetAll(Dignosis dignosis);
        Task<List<Dignosis>> BindDignosisCatagoryName(Dignosis dignosis);
        Task<Dignosis> GetOne(int DignosisID);
        Task<int> Create(Dignosis entity);
        Task<int> Delete(int DignosisID);
    }
}
