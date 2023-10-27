using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Sale_Management.Articles
{
    public interface IArticleService:IApplicationService
    {
        Task<PagedResultDto<ArticleDto>> GetAllAsync();
        Task<ArticleDto> GetByIdAsync(Guid id);
        Task<ArticleDto> CreateAsync(CreateArticleDto article);
        Task UpdateAsync(Guid id, UpdateArticleDto Newarticle);
        Task DeleteAsync(Guid id);
    }
}
