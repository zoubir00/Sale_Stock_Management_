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
        private readonly VenteService _Ventservice;

        public VenteController(IVenteService service, VenteService ventservice)
        {
            _service = service;
            _Ventservice = ventservice;
        }
        // get Client ventes
        [HttpGet]
        public IActionResult GetClientVentes(string ClientfName, string ClientlName)
        {
           var ventes= _Ventservice.GetClientVentes(ClientfName,ClientlName);
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
