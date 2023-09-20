using Sale_Management.BaseService;
using Sale_Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale_Management.Articles
{
    public class ArticleService :GenericRepository<ArticleDto>, IArticleService
    {
        public ArticleService(Sale_ManagementDbContext dbContext):base(dbContext)
        {
            
        }
    }
}
