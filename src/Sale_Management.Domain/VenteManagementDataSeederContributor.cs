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
        public VenteManagementDataSeederContributor(/*IRepository<Client>*/)
        {
            
        }
        public Task SeedAsync(DataSeedContext context)
        {
            throw new NotImplementedException();
        }
    }
}
