using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Sale_Management.Entities
{
    public class Article:Entity<Guid>
    {
        public string? Libelle { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public int QuantityinStock { get; set; }

        public Article(Guid id, string libelle, string description, string image, double price, int quantityinStock) : base(id)
        {
            Libelle = libelle;
            Description = description;
            Image = image;
            Price = price;
            QuantityinStock = quantityinStock;
        }
    }
}
