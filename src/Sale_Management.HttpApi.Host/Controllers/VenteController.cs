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

        //Post
        [HttpPost("vente")]
        public async Task<IActionResult> CreateVente(int clientId,CreateUpdateVenteDto vente)
        {
            try
            {
                var _vente = await _service.AddVente(clientId, vente);
                return Ok(_vente);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
