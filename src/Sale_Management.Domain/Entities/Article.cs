using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Sale_Management.Entities
{
    public class Article:Entity<int>
    {
        public string Libelle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int QuantityinStock { get; set; }
        
    }
}
