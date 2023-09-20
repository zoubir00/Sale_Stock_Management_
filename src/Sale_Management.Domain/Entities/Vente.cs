using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Sale_Management.Entities
{
    public class Vente:Entity<int>
    {
        public DateTime DataVente { get; set; }
        public int TotalAmount { get; set; }
        //navigation
        public List<VenteArticles> VenteItems { get; set; }
        public Client Client { get; set; }
    }
}
