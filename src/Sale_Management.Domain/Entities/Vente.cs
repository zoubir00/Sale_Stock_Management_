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
        public Guid clientId { get; set; }
        [ForeignKey(nameof(clientId))]
        public Client? client { get; set; }
        public int QtyTotal { get; set; }
        public double TotalAmount { get; set; }
        public List<VenteLine>? VenteLines { get; set; }

        public Vente(DateTime dateVente, Guid clientId)
        {
            Id = Guid.NewGuid();
            DateVente = dateVente;
            this.clientId = clientId;
            QtyTotal = 0;
            TotalAmount = 0;
            VenteLines = new List<VenteLine>();
        }

        //internal Vente(Guid id, DateTime dateVente, Guid ClientId,int qtyTotal,double totalAmount): base(id)
        //{
        //    DateVente = dateVente;
        //    clientId = ClientId;
        //    QtyTotal = qtyTotal;
        //    TotalAmount = totalAmount;
        //}
    }
    
}
