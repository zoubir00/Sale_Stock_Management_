using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Sale_Management.Clients
{
    public interface IClientService: IApplicationService
    {
        Task<PagedResultDto<ClientDto>> GetAllAsync();
        Task<ClientDto> GetByIdAsync(Guid id);
        Task<ClientDto> CreateAsync(CreateClientDto client);
        Task UpdateAsync(Guid id, UpdateClientDto Newclient);
        Task DeleteAsync(Guid id);
    }
}
