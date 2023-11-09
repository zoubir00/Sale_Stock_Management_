
using Sale_Management.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Sale_Management
{
    public class VenteManagementDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Client, Guid> _clientRepository;
        public VenteManagementDataSeederContributor(IRepository<Client, Guid> clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            await _clientRepository.InsertAsync(
                new Client(
                
                    Guid.NewGuid(),
                     "Walid",
                     "Azhar",
                    "walid@test.com",
                     "0789876535"
                     )
                ,
                autoSave: true
                );
        }
    }
}
