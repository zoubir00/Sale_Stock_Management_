using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Sale_Management.Entities
{
    public class VenteArticles:Entity<int>
    {
        public int Quantity { get; set; }
        public int ItemAmount { get; set; }
        //navigation
        public int VenteId { get; set; }
        [ForeignKey(nameof(VenteId))]
        public Vente Vente { get; set; }
        public int ArticleId { get; set; }
        [ForeignKey(nameof(ArticleId))]
        public Article Article { get; set; }
    }
}
