using Sale_Management.IBaseServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sale_Management.Articles
{
    public interface IArticleService:IGenericRepository<ArticleDto>
    {
    }
}
