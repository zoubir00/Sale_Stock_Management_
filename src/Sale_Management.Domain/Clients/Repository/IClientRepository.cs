using Sale_Management.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Sale_Management.Clients.Repository
{
    public interface IClientRepository : IRepository<Client, int>
    {
       
    }
}
