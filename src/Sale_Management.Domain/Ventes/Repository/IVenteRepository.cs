using Sale_Management.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Sale_Management.Ventes.Repository
{
    public interface IVenteRepository: IRepository<Vente,Guid>
    {
        
    }
}
