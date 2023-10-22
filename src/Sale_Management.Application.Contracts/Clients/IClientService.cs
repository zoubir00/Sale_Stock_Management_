using Sale_Management.IBaseServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Sale_Management.Clients
{
    public interface IClientService: IApplicationService
    {
        Task<List<ClientDto>> GetAllAsync();
        Task<ClientDto> GetByIdAsync(int id);
        Task<ClientDto> CreateAsync(ClientDto entity);
        Task<ClientDto> UpdateAsync(int id, ClientDto entity);
        Task DeleteAsync(int id);
    }
}
