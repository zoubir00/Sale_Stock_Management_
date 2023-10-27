using Sale_Management.Articles;
using Sale_Management.Ventes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Sale_Management.VenteLines
{
    public class VenteLinesDto:EntityDto<Guid>
    {
        public Guid? VenteCode { get; set; }
        public Guid articleId { get; set; }
        public string? articlelebelle { get; set; }
        public int QtySold { get; set; }
        public double SalePrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
