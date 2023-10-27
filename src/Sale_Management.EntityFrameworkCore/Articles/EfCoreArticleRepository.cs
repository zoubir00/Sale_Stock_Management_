using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Sale_Management.Articles.Repository;
using Sale_Management.Entities;
using Sale_Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Sale_Management.Articles
{
    public class EfCoreArticleRepository : EfCoreRepository<Sale_ManagementDbContext, Article, Guid>, IArticleRepository
    {
        [Obsolete]
        private readonly IHostingEnvironment _host;

        [Obsolete]
        public EfCoreArticleRepository(IDbContextProvider<Sale_ManagementDbContext> dbContextProvider, IHostingEnvironment host ) : base(dbContextProvider)
        {
            _host = host;
        }

        [Obsolete]
        public async Task<Article> createArticleasync(string libelle, string description, IFormFile img, double price, int quantityinStock)
        {
            var dbSet = await GetDbSetAsync();
            var filePath = Path.Combine(@"C:\\Users\\HP\\OneDrive\\Bureau\\Internship\\AbpTutorial\\GestionVente\\angular\\src\\assets\\images\\articles", img.FileName);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                img.CopyTo(stream);
            }

            var article = new Article
            (
                Guid.NewGuid(),
                libelle,
                description,
                img.FileName,
                price,
                quantityinStock
            );
            await dbSet.AddAsync(article);
            return article ;
        }

        public async Task<Article> GetByIdAsync(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
