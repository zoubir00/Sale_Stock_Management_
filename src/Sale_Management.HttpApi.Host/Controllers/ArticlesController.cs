using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sale_Management.Articles;
using Sale_Management.Entities;
using Sale_Management.EntityFrameworkCore;
using Sale_Management.UploadService;
using Sale_Management.Ventes;
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
        private readonly IArticleService _service;
        //[Obsolete]
        //private readonly IHostingEnvironment _host;
        private readonly UploadImageService _uploadImage;


       
        public ArticlesController(/*IHostingEnvironment host, */IArticleService service, UploadImageService uploadImage)
        {



            //_host = host;
            _service = service;
            _uploadImage = uploadImage;
        }
        // get All article
        [HttpGet("GetArticles")]
        public async Task<ActionResult> GetAllArticle()
        {
            var _article = await _service.GetAllAsync();
            return Ok(_article);
        }
        //// get article with by id
        [HttpGet("article/{id}")]
        public async Task<ActionResult> GetArticleById(Guid id)
        {
            var _article = await _service.GetByIdAsync(id);
            if (_article != null)
            {
                return Ok(_article);
            }
            return NotFound();
        }
        //// Insert article
        [HttpPost("CreateArticle")]
        public async Task<IActionResult> CreateArticle([FromForm] CreateArticleDto article, IFormFile img)
        {
            //var filePath = Path.Combine(_host.WebRootPath + "/images/articles", img.FileName);
            article.Image = await _uploadImage.UploadImage(img);
            var _article = await _service.CreateAsync(article);
            return Ok(_article);
        }
        //// edit article

        [HttpPut("editArticle/{id}")]
        public async Task<IActionResult> UpdateArticle(Guid id, [FromForm] UpdateArticleDto article, IFormFile img)
        {

            var existarticle = await _service.GetByIdAsync(id);

            if (existarticle == null)
            {
                throw new Exception("Article doesn't exist");
            }
            if (img != null || img.FileName.ToLower() != existarticle.Image.ToLower())
            {
                article.Image =await _uploadImage.UploadImage(img);
            }
            else
            {
                article.Image = existarticle.Image;
            }
             await _service.UpdateAsync(id, article);
             return Ok(article);
        }
        //// delete book
        [HttpDelete("deleteArticle/{id}")]
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
           await _service.DeleteAsync(id);
            return Ok();
        }


    }
}
