using Sale_Management.BaseService;
using Sale_Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale_Management.Clients
{
    public class ClientService : GenericRepository<ClientDto>, IClientService
    {
        public ClientService(Sale_ManagementDbContext dbContext):base(dbContext){}
    }
}
