using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sale_Management.Articles;
using Sale_Management.Entities;
using Sale_Management.EntityFrameworkCore;
using Sale_Management.IBaseServices;
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
        private readonly IWebHostEnvironment env;

        public ArticlesController(IGenericRepository<Article> service, Sale_ManagementDbContext dbContext, IWebHostEnvironment environment)
        {
            _service = service;
            _dbContext = dbContext;
            this.env = environment;
        }
        // get All article
        [HttpGet("GetArticles")]
        public async Task<ActionResult> GetAllArticle()
        {
            var _article = await _service.GetAllAsync();
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
        public IActionResult CreateArticle([FromForm]ArticleDto article)
        {
            var contentPath = this.env.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var ext = Path.GetExtension(article.Image.FileName);
            var allowedExtension = new string[] { ".jpg", ".png", ".jpeg" };
            if (!allowedExtension.Contains(ext))
            {
                string msg = string.Format("only {0} extensions are allowed", string.Join(",", allowedExtension));
                return BadRequest();
            }
            string uniquestring = Guid.NewGuid().ToString();
            var newFileName = uniquestring + ext;
            var fullPath = Path.Combine(path, newFileName);
            var stream = new FileStream(fullPath, FileMode.Create);
            article.Image.CopyTo(stream);
            stream.Close();
            var _article = new Article
            {
               Libelle=article.Libelle,
               Image=newFileName,
               Description=article.Description,
               Price=article.Price,
               QuantityinStock=article.QuantityinStock
               
            };
            _service.CreateAsync(_article);
            return Ok();
        }
        // edit article
        
        [HttpPut("editArticle/{id}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromForm] ArticleDto article)
        {
            var contentPath = this.env.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var ext = Path.GetExtension(article.Image.FileName);
            var allowedExtension = new string[] { ".jpg", ".png", ".jpeg" };
            if (!allowedExtension.Contains(ext))
            {
                string msg = string.Format("only {0} extensions are allowed", string.Join(",", allowedExtension));
                return BadRequest();
            }
            string uniquestring = Guid.NewGuid().ToString();
            var newFileName = uniquestring + ext;
            var fullPath = Path.Combine(path, newFileName);
            var stream = new FileStream(fullPath, FileMode.Create);
            article.Image.CopyTo(stream);
            stream.Close();
            var existarticle = await _dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);
            if (existarticle == null)
            {
                throw new Exception("Article doesn't exist");
            }
            // update the article
            existarticle.Libelle = article.Libelle;
            existarticle.Image = newFileName;
            existarticle.Description = article.Description;
            existarticle.Price = article.Price;
            existarticle.QuantityinStock = article.QuantityinStock;
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
