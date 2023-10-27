using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Http;
using Sale_Management.Articles.Repository;
using Sale_Management.Clients.Repository;
using Sale_Management.Entities;
using Sale_Management.EntityFrameworkCore;
using Sale_Management.UploadService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Sale_Management.Articles
{
    public class ArticleService : ApplicationService,IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ArticleManager _articleManager ;
        private readonly UploadImageService _uploadImage;

        public ArticleService(IArticleRepository articleRepository, ArticleManager articleManager, UploadImageService uploadImage)
        {
            _articleRepository = articleRepository;
            _articleManager = articleManager;
            _uploadImage = uploadImage;
        }

        // Create article

        [Obsolete]
        public async Task<ArticleDto> CreateAsync(CreateArticleDto article)
        {
            try
            {
                //var filePath = Path.Combine(_host.WebRootPath + "/images/articles", img.FileName);
                //var filePath = Path.Combine(@"C:\\Users\\HP\\OneDrive\\Bureau\\Internship\\AbpTutorial\\GestionVente\\angular\\src\\assets\\images\\articles", article.Image.FileName);
                //using (FileStream stream = new FileStream(filePath, FileMode.Create))
                //{
                //    article.Image.CopyTo(stream);
                //}
               
                var _article = await _articleManager.CreateArticletAsync(article.Libelle, article.Description, article.Image, article.Price, article.QuantityinStock);
               var savedArticle= await _articleRepository.InsertAsync(_article);
                return ObjectMapper.Map<Article, ArticleDto>(savedArticle);
                           
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // delete article
        public async Task DeleteAsync(Guid id)
        {
            var articleToDelete = await _articleRepository.GetByIdAsync(id);
            await _articleRepository.DeleteAsync(articleToDelete);
        }


        // get all article
        public async Task<PagedResultDto<ArticleDto>> GetAllAsync()
        {
            try
            {
                var articles = await _articleRepository.GetListAsync();
                var totalCount = await _articleRepository.CountAsync();
                return new PagedResultDto<ArticleDto>(
                            totalCount,
                            ObjectMapper.Map<List<Article>, List<ArticleDto>>(articles)
                            );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // get article by ID
        public async Task<ArticleDto> GetByIdAsync(Guid id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            return ObjectMapper.Map<Article, ArticleDto>(article);
        }

        // update article
        public async Task UpdateAsync(Guid id, UpdateArticleDto Newarticle)
        {
            var existArticle = await _articleRepository.GetByIdAsync(id);
            if (existArticle == null)
            {
                throw new Exception("Article not found");
            }
            existArticle.Libelle = Newarticle.Libelle;
            existArticle.Description = Newarticle.Description;
            //if (Newarticle.Image != null || Newarticle.Image?.FileName.ToLower() != existArticle.Image?.ToLower())
            //{
            //    var filePath = Path.Combine(@"C:\\Users\\HP\\OneDrive\\Bureau\\Internship\\AbpTutorial\\GestionVente\\angular\\src\\assets\\images\\articles", Newarticle.Image.FileName);
            //    using (FileStream stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        Newarticle.Image.CopyTo(stream);
            //    }
            //    existArticle.Image = Newarticle.Image.FileName;
            //}
            //else
            //{
            //    existArticle.Image = Newarticle.Image.FileName;
            //}
            existArticle.Image = Newarticle.Image;
            existArticle.Price = Newarticle.Price;
            existArticle.QuantityinStock = Newarticle.QuantityinStock;

            await _articleRepository.UpdateAsync(existArticle);
        }

       
    }
}
