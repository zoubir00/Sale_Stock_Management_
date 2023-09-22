using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Sale_Management.Entities
{
    public class Vente:Entity<int>
    {
        public DateTime DateVente { get; set; }
        public int articleId { get; set; }
        [ForeignKey(nameof(articleId))]
        public Article articleVendue { get; set; }
         public int clientId { get; set; }
        [ForeignKey(nameof(clientId))]
        public Client client { get; set; }
        public int  QuantityVendue { get; set; }
        public double PrixTotal(double articlePrice)
        {
            return articlePrice * QuantityVendue;
        }
    }
}
