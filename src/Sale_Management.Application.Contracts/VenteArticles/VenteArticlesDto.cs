using Sale_Management.Articles;
using Sale_Management.Ventes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sale_Management.VenteArticles
{
    public class VenteArticlesDto
    {

        public int Amount { get; set; }
        public double Price { get; set; }
        //navigation
        public int VenteId { get; set; }
        [ForeignKey(nameof(VenteId))]
        public CreateUpdateVenteDto Vente { get; set; }
        public int ArticleId { get; set; }
        [ForeignKey(nameof(ArticleId))]
        public ArticleDto Article { get; set; }


    }
}
