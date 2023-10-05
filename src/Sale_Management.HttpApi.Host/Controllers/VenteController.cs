using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale_Management.VenteLines;
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
        //[HttpGet("ventes")]
        //public async Task<IActionResult> GetVentes()
        //{
        //    var ventes = _service.GetVenteSummaries();
        //    if (ventes == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(ventes);
        //}

        //// get Client ventes
        //[HttpGet("GetVenteByClient")]
        //public async Task<IActionResult> GetClientVentes(string ClientfName, string ClientlName)
        //{
        //   var ventes=await _service.GetVentesByClientNameAsync(ClientfName,ClientlName);
        //    if (ventes==null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(ventes);
        //}
        ////Post
        [HttpPost("vente")]
        public IActionResult AddVente(string venteCode, DateTime dateVente, int clientId, List<VenteLinesDto> venteLines)
        {
            try
            {
                var ventesum = _service.CreateVente(venteCode, dateVente, clientId, venteLines);
                return Ok(ventesum);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        //// delete method:
        //[HttpDelete("ConfirmDelete/{id}")]
        //public  IActionResult Delete(int id)
        //{
        //   var t= _service.deleteVente(id);
        //    if (t.IsCompletedSuccessfully)
        //    {
        //        return Ok();
        //    }
        //    return NotFound();
        //}
    }
}
