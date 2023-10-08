using Microsoft.EntityFrameworkCore;
using Sale_Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale_Management.VenteLines
{
    public class VenteLineService :IVenteLineService
    {
        // delete a given venteLine of a vente 
        private readonly Sale_ManagementDbContext _dbContext;

        public VenteLineService(Sale_ManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

      
    }
}
