using Sale_Management.BaseService;
using Sale_Management.Clients.Repository;
using Sale_Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Sale_Management.Clients
{
    public class ClientService : ApplicationService ,IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ClientManager _clientManager;
        public ClientService(IClientRepository clientRepository)
        {
            _clientManager = clientRepository;
        }
        public Task<ClientDto> CreateAsync(ClientDto entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ClientDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> UpdateAsync(int id, ClientDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
