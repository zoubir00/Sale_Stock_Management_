using Microsoft.EntityFrameworkCore;
using Sale_Management.Clients.Repository;
using Sale_Management.Entities;
using Sale_Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Sale_Management.Clients
{
    public class EfCoreClientRepository : EfCoreRepository<Sale_ManagementDbContext, Client, Guid>, IClientRepository
    {
        public EfCoreClientRepository(IDbContextProvider<Sale_ManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            var DbSet = await GetDbSetAsync();
            return await DbSet.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
