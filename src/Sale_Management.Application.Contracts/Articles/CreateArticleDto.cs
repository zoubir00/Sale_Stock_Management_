using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sale_Management.Articles
{
    public class CreateArticleDto
    {
        public string? Libelle { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public int QuantityinStock { get; set; }
    }
}
