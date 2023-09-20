using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sale_Management.Clients;
using Sale_Management.Entities;
using Sale_Management.IBaseServices;
using System.Threading.Tasks;

namespace Sale_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IGenericRepository<Client> _service;

        public ClientsController(IGenericRepository<Client> service)
        {
            _service = service;
        }
        // get All clients
        [HttpGet]
        public async Task<ActionResult> GetAllClients()
        {
            var clients = await _service.GetAllAsync();
            return Ok(clients);
        }
        // get client with by id
        [HttpGet("Client/{id}")]
        public async Task<ActionResult> GetAllClients(int id)
        {
            var client = await _service.GetByIdAsync(id);
            if (client != null)
            {
                return Ok(client);
            }
            return NotFound();
        }
        // Insert Client
        [HttpPost]
        public IActionResult CreateClient(ClientDto client)
        {
            var _client = new Client
            {
                FName = client.FName,
                LName = client.LName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };
            _service.CreateAsync(_client);
            return Ok();
        }
        // edit book
        [HttpPut]
        public IActionResult UpdateClient(ClientDto client)
        {

            var _client = new Client
            {
                FName = client.FName,
                LName = client.LName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };
            _service.UpdateAsync(_client);
            return Ok();
        }
    }
}
