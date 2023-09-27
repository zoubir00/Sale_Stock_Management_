using Sale_Management.Articles;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Sale_Management.Ventes
{
    public class VeteGetDto : EntityDto<int>
    {
        public DateTime DateVente { get; set; }
        public List<ArticleDtoForGet> articleVendue { get; set; }

        public string clientName { get; set; }
        public int QuantityVendue { get; set; }
        public double prixTotal { get; set; }
    }
    public class ArticleDtoForGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuantityVendue { get; set; }
        public double TotalAmount { get; set; }
    }
}
