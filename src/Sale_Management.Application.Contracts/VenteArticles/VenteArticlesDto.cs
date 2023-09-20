using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sale_Management.VenteArticles
{
    public class VenteArticlesDto
    {
        public int Quantity { get; set; }
        public int ItemAmount { get; set; }
        //navigation
        public int VenteId { get; set; }
      
        public int ArticleId { get; set; }
       
    }
}
