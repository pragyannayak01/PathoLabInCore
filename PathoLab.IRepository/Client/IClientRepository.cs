using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PathoLab.Domain.Client;


namespace PathoLab.IRepository.Client
{
    public interface IClientRepository
    {
        Task<List<ClientMaster>> GetAll(ClientMaster client);
        Task<ClientMaster> GetOne(int clientid);
        Task<int> Create(ClientMaster entity);
        //Task<int> Update(ClientMaster entity);
        Task<int> Delete(int id);
    }
}
