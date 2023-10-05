using Sale_Management.Articles;
using Sale_Management.Clients;
using Sale_Management.VenteArticles;
using Sale_Management.VenteLines;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Json.SystemTextJson.JsonConverters;

namespace Sale_Management.Ventes
{
    public class VenteDto:EntityDto<string>
    {
        public DateTime DateVente { get; set; }
        public int clientId { get; set; }
        [ForeignKey(nameof(clientId))]
        public ClientDto? client { get; set; }
        public int QtyTotal { get; set; }
        public double TotalAmount { get; set; }
        public List<VenteLinesDto>? VenteLines { get; set; }


    }
}
