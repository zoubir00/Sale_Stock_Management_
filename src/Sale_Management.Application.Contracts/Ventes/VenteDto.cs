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
    public class VenteDto:AuditedEntityDto<Guid>
    {
        public DateTime DateVente { get; set; }
        public Guid clientId { get; set; }
        public string? clientName { get; set; }
        public string? clientPhoneNumber { get; set; }
        public int QtyTotal { get; set; }
        public double TotalAmount { get; set; }

        public List<VenteLinesDto>? VenteLines { get; set; }


    }
}
