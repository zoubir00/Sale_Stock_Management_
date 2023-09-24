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
        public async Task<ActionResult> GetClientById(int id)
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
        // edit client
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateClient(int id,[FromBody]ClientDto client)
        {
            if (id == client.Id)
            {
                var existClient = await _service.GetByIdAsync(id);
                existClient.FName = client.FName;
                existClient.LName = client.LName;
                existClient.Email = client.Email;
                existClient.PhoneNumber = client.PhoneNumber;
                await _service.UpdateAsync(id, existClient);
                
                return Ok(client);
            }

            return BadRequest();
        }
        // edit book
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteClient(int id)
        {
            _service.DeleteAsync(id);
            return Ok();
        }
    }
}
