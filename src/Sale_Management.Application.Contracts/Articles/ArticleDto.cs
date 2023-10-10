using Sale_Management.IBaseServices;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace Sale_Management.Articles
{
    public class ArticleDto: EntityDto<int>,IEntityBase
    {
        public string? Libelle { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public int QuantityinStock { get; set; }
    }
}
