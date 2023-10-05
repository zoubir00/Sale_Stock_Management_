using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Sale_Management.Entities
{
    public class Vente:Entity<Guid>
    {
        public DateTime DateVente { get; set; }
        public int clientId { get; set; }
        [ForeignKey(nameof(clientId))]
        public Client? client { get; set; }
        public int  QtyTotal { get; set; }
        public double TotalAmount { get; set; }
        public List<VenteLines>? VenteLines { get; set; }
    }
}
