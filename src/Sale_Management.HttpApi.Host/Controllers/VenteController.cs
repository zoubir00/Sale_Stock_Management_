using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale_Management.Ventes;
using System;
using System.Collections.Generic;
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
        [HttpGet("GetVenteByClient")]
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
        public  IActionResult AddVente([FromBody] InputVente input)
        {
            try
            {
               var ventesum= _service.CreateVente(input.clientId, input.articleIds,input.quantities);
                return Ok(ventesum);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // delete method:
        [HttpDelete("ConfirmDelete/{id}")]
        public  IActionResult Delete(int id)
        {
           var t= _service.deleteVente(id);
            if (t.IsCompletedSuccessfully)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
