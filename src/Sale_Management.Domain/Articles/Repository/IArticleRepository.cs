using Volo.Abp.Domain.Repositories;
using Sale_Management.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Sale_Management.Articles.Repository
{
    public interface IArticleRepository: IRepository<Article, Guid>
    {
        Task<Article> GetByIdAsync(Guid id);
        Task<Article> createArticleasync(string libelle, string description, IFormFile img, double price, int quantityinStock);
    }
}
