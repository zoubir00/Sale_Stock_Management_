using Microsoft.EntityFrameworkCore;
using Sale_Management.Entities;
using Sale_Management.EntityFrameworkCore;
using Sale_Management.Ventes.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Sale_Management.Ventes
{
    public class EfCoreVenteRepository : EfCoreRepository<Sale_ManagementDbContext, Vente, Guid>, IVenteRepository
    {
        public EfCoreVenteRepository(IDbContextProvider<Sale_ManagementDbContext> dbContextProvider):base(dbContextProvider)
        {
            
        }
        //public async Task<List<Vente>> GetVentesAsync()
        //{
        //    var dbset = await GetDbSetAsync();
        //    var ventes = await dbset.Include(v => v.client).ToListAsync();
        //    return ventes;
        //}
    }
}
