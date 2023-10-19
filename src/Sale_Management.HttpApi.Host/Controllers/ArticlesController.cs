using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sale_Management.Articles;
using Sale_Management.Entities;
using Sale_Management.EntityFrameworkCore;
using Sale_Management.IBaseServices;
using Sale_Management.UploadService;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly Sale_ManagementDbContext _dbContext;
        private readonly IGenericRepository<Article> _service;
        [Obsolete]
        private readonly IHostingEnvironment _host;

        [Obsolete]
        public ArticlesController(IGenericRepository<Article> service, Sale_ManagementDbContext dbContext, IHostingEnvironment host)
        {
            _service = service;
            _dbContext = dbContext;
           
            _host = host;
        }
        // get All article
        [HttpGet("GetArticles")]
        public async Task<ActionResult> GetAllArticle()
        {
            var _article = await _dbContext.Articles.ToListAsync();
            //var article = _article.Select(a=>new ArticleDto
            //{
            //    Id=a.Id,
            //    Libelle=a.Libelle,
            //    Description=a.Description,
            //    Image=@"C:\Users\HP\OneDrive\Bureau\Internship\AbpTutorial\GestionVente\angular\src\assets\images\articles\"+ a.Image,
            //    Price=a.Price,
            //    QuantityinStock=a.QuantityinStock
            //});
            return Ok(_article);
        }
        // get article with by id
        [HttpGet("article/{id}")]
        public async Task<ActionResult> GetArticleById(int id)
        {
            var _article = await _service.GetByIdAsync(id);
            if (_article != null)
            {
                return Ok(_article);
            }
            return NotFound();
        }
        // Insert article
        [HttpPost("CreateArticle")]
        public IActionResult CreateArticle([FromForm] ArticleDto article,IFormFile img)
        {
            //var filePath = Path.Combine(_host.WebRootPath + "/images/articles", img.FileName);
            var filePath = Path.Combine(@"C:\\Users\\HP\\OneDrive\\Bureau\\Internship\\AbpTutorial\\GestionVente\\angular\\src\\assets\\images\\articles", img.FileName);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                img.CopyTo(stream);
            }
          
            var _article = new Article
            {
               Libelle=article.Libelle,
               Image=img.FileName,
               Description=article.Description,
               Price=article.Price,
               QuantityinStock=article.QuantityinStock
            };
            _service.CreateAsync(_article);
            return Ok(_article);
        }
        // edit article
        
        [HttpPut("editArticle/{id}")]
        public async Task<IActionResult> UpdateArticle(int id,[FromForm]ArticleDto article, IFormFile img)
        {
           
            var existarticle = await _dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (existarticle == null)
            {
                throw new Exception("Article doesn't exist");
            }

            // update the article
            existarticle.Libelle = article.Libelle;
            existarticle.Description = article.Description;
            existarticle.Price = article.Price;
            existarticle.QuantityinStock = article.QuantityinStock;
            if (img != null || img.FileName.ToLower() != existarticle.Image.ToLower())
            {
                var filePath = Path.Combine(@"C:\\Users\\HP\\OneDrive\\Bureau\\Internship\\AbpTutorial\\GestionVente\\angular\\src\\assets\\images\\articles", img.FileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
            }
            existarticle.Image =img.FileName;
            _dbContext.Entry(existarticle).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok(article);
        }
        // delete book
        [HttpDelete("deleteArticle/{id}")]
        public IActionResult DeleteArticle(int id)
        {
            _service.DeleteAsync(id);
            return Ok();
        }

        // get By Libelli 
        [HttpGet("articleLibelli")]
        public IActionResult Search(string Slibelle)
        {
            var article = _dbContext.Articles.Where(a => a.Libelle.Contains(Slibelle)).Select(
                article => new ArticleDto
                {
                    Id=article.Id,
                    Libelle = article.Libelle,
                    //Image = article.Image,
                    Description = article.Description,
                    Price = article.Price,
                    QuantityinStock = article.QuantityinStock

                }).ToList();
            return Ok(article);
        }
        // Most articles sold
        [HttpGet("mostSold")]
        public async Task<IActionResult> ArticlesSold()
        {
            var articles = await _dbContext.Articles.Where(a => a.QuantityinStock <= 10)
            .Select(article => new ArticleDto
            {
                Id = article.Id,
                Libelle = article.Libelle,
                //Image = article.Image,
                Description = article.Description,
                Price = article.Price,
                QuantityinStock = article.QuantityinStock
            }).ToListAsync();
            return Ok(articles);
        }
    }
}
