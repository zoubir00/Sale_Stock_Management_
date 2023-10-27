using Sale_Management.Clients.Repository;
using Sale_Management.Entities;
using Sale_Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Sale_Management.Clients
{
    public class ClientService : ApplicationService ,IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ClientManager _clientManager;

        public ClientService(IClientRepository clientRepository, ClientManager clientManager)
        {
            _clientRepository = clientRepository;
            _clientManager = clientManager;
        }

        public async Task<ClientDto> CreateAsync(CreateClientDto client)
        {
            try
            {   
                 var _client = await _clientManager.CreateClientAsync(client.FName, client.LName, client.Email, client.PhoneNumber);
                 await _clientRepository.InsertAsync(_client);
                 return ObjectMapper.Map<Client, ClientDto>(_client);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task DeleteAsync(Guid id)
        {
           
            await _clientRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<ClientDto>> GetAllAsync()
        {
            var clients = await _clientRepository.GetListAsync();
            if (clients.Count == 0)
            {
                throw new NoClientFoundException();
            }
            var totalCount = await _clientRepository.CountAsync();
            return new PagedResultDto<ClientDto>(
                totalCount,
                ObjectMapper.Map<List<Client>, List<ClientDto>>(clients)
                );
        }

        public async Task<ClientDto> GetByIdAsync(Guid id)
        {
            var client = await _clientRepository.GetAsync(id);
            return ObjectMapper.Map<Client, ClientDto>(client);
        }

        public async Task UpdateAsync(Guid id, UpdateClientDto Newclient)
        {
            var existClient = await _clientRepository.GetAsync(id);
            if (existClient == null)
            {
                throw new Exception("Client not found");
            }
            existClient.FName = Newclient.FName;
            existClient.LName = Newclient.LName;
            existClient.Email = Newclient.Email;
            existClient.PhoneNumber = Newclient.PhoneNumber;

            await _clientRepository.UpdateAsync(existClient);

        }
    }
}
