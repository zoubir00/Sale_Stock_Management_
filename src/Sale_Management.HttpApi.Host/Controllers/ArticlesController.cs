using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale_Management.Articles;
using Sale_Management.Entities;
using Sale_Management.IBaseServices;
using System;
using System.Threading.Tasks;

namespace Sale_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IGenericRepository<Article> _service;

        public ArticlesController(IGenericRepository<Article> service)
        {
            _service = service;
        }
        // get All article
        [HttpGet]
        public async Task<ActionResult> GetAllArticle()
        {
            var _article = await _service.GetAllAsync();
            return Ok(_article);
        }
        // get article with by id
        [HttpGet("Client/{id}")]
        public async Task<ActionResult> GetAllArticle(int id)
        {
            var _article = await _service.GetByIdAsync(id);
            if (_article != null)
            {
                return Ok(_article);
            }
            return NotFound();
        }
        // Insert article
        [HttpPost]
        public IActionResult CreateArticle(ArticleDto article)
        {
            var _article = new Article
            {
               Libelle=article.Libelle,
               Image=article.Image,
               Description=article.Description,
               Price=article.Price,
               QuantityinStock=article.QuantityinStock
               
            };
            _service.CreateAsync(_article);
            return Ok();
        }
        // edit article
        
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] ArticleDto article)
        {
            if (id == article.Id)
            {
                var existarticle = await _service.GetByIdAsync(id);
                existarticle.Libelle = article.Libelle;
                existarticle.Image = article.Image;
                existarticle.Description = article.Description;
                existarticle.Price = article.Price;
                existarticle.QuantityinStock = article.QuantityinStock;
                await _service.UpdateAsync(id, existarticle);
                return Ok(article);
            }

            return BadRequest();
        }
        // delete book
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteClient(int id)
        {
            _service.DeleteAsync(id);
            return Ok();
        }
    }
}
