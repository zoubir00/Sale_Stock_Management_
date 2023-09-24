using Sale_Management.Articles;
using Sale_Management.Clients;
using Sale_Management.VenteArticles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Json.SystemTextJson.JsonConverters;

namespace Sale_Management.Ventes
{
    public class VenteDto:EntityDto<int>
    {
        public DateTime DateVente { get; set; }
        public int articleId { get; set; }
        [ForeignKey(nameof(articleId))]
        public ArticleDto articleVendue { get; set; }
        public int clientId { get; set; }
        [ForeignKey(nameof(clientId))]
        public ClientDto client { get; set; }
        public int QuantityVendue { get; set; }
        public double prixTotal { get; set; }
       

    }
}
