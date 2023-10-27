using Sale_Management.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Sale_Management.Articles
{
    public class ArticleManager:DomainService
    {
        public async Task<Article> CreateArticletAsync(string libelle,
            string description,
            string image,
            double price,
            int quantityInStock)
        {
            
            return new Article(
               GuidGenerator.Create(),
                libelle,
                description,
                image,
                price,
                quantityInStock
                );
        }
    }
}
