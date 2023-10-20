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
        //// get Ventes
        [HttpGet("ventes")]
        public IActionResult GetVentes()
        {
            var ventes = _service.GetAllVentes();
            if (ventes == null)
            {
                return NotFound();
            }
            return Ok(ventes);
        }

        ////// get Client ventes
        [HttpGet("{codeVente}")]
        public IActionResult VenteDetails(Guid codeVente)
        {
            var vente = _service.GetVenteDetails(codeVente);
            if (vente == null)
            {
                return NotFound();
            }
            return Ok(vente);
        }
        ////////Post create vente
        [HttpPost("vente")]
        public IActionResult AddVente( DateTime dateVente, int clientId, List<VenteLinesDto> venteLines)
        {
            try
            {
                var ventesum = _service.CreateVente( dateVente, clientId, venteLines);
                return Ok(ventesum);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //// edit vente
        [HttpPut]
        public IActionResult UpdateVente(Guid venteCode, DateTime newDateVente, int newcClientId, List<VenteLinesDto> venteLines)
        {
            try
            {
                var updatedVente = _service.EditVente(venteCode, newDateVente, newcClientId, venteLines);
                return Ok(updatedVente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        ////add vete line
        [HttpPost("newVenteLine")]
        public IActionResult AddVenteLine(Guid venteCode, VenteLinesDto newVenteLineDto)
        {
            try
            {
                _service.AddVenteLineToVente(venteCode, newVenteLineDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //// delete vente
        [HttpDelete("{codeVente}")]
        public IActionResult Delete(Guid codeVente)
        {
            try
            {
                _service.DeleteVente(codeVente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }

        //// delete vente line :
        [HttpDelete("ventelines/{venteLineId}")]
        public IActionResult Delete(Guid codeVente, int venteLineId)
        {
            try
            {
                _service.DeleteVenteLine(codeVente, venteLineId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }
    }
}
