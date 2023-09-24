using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale_Management.Ventes;
using System;
using System.Threading.Tasks;

namespace Sale_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenteController : ControllerBase
    {
        private readonly IVenteService _service;
        

        public VenteController(IVenteService service)
        {
            _service = service;
            
        }
        // get Ventes
        [HttpGet("ventes")]
        public async Task<IActionResult> GetVentes()
        {
            var ventes =await _service.GetAllVentesAsync();
            if (ventes == null)
            {
                return NotFound();
            }
            return Ok(ventes);
        }

        // get Client ventes
        [HttpGet]
        public async Task<IActionResult> GetClientVentes(string ClientfName, string ClientlName)
        {
           var ventes=await _service.GetVentesByClientNameAsync(ClientfName,ClientlName);
            if (ventes==null)
            {
                return NotFound();
            }
            return Ok(ventes);
        }
        //Post
        [HttpPost("vente")]
        public  IActionResult CreateVente(int clientId,int articleId,int quantity)
        {
            try
            {
                _service.AddVente(clientId, articleId,quantity);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
