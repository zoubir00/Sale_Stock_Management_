using Sale_Management.Clients;
using Sale_Management.VenteArticles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sale_Management.Ventes
{
    public class CreateUpdateVenteDto
    {
        public DateTime DataVente { get; set; }
        public double TotalAmount { get; set; }
        //navigation
        public List<VenteArticlesDto> VenteItems { get; set; }
        public int clientId { get; set; }
        
    }
}
