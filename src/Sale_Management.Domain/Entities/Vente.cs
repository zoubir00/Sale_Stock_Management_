using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Sale_Management.Entities
{
   
    public class Vente : AuditedAggregateRoot<Guid>
    {
        public DateTime DateVente { get; set; }
        public int clientId { get; set; }
        [ForeignKey(nameof(clientId))]
        public Client? client { get; set; }
        public int QtyTotal { get; set; }
        public double TotalAmount { get; set; }
        public List<VenteLine> VenteLines { get; set; }
    }
    
}
