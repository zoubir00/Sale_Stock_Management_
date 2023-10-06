using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Sale_Management.Entities
{
    public class VenteLine : Entity<int>
    {
        public string VenteCode { get; set; }

        [ForeignKey(nameof(VenteCode))]
        public Vente? Vente { get; set; }

        public int articleId { get; set; }
        [ForeignKey(nameof(articleId))]
        public Article? Article { get; set; }

        public int QtySold { get; set; }
        public double TotalPrice { get; set; }
    }
}
