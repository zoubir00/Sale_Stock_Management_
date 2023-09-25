using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sale_Management.Clients;
using Sale_Management.Entities;
using Sale_Management.EntityFrameworkCore;
using Sale_Management.IBaseServices;
using System.Linq;
using System.Threading.Tasks;

namespace Sale_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly Sale_ManagementDbContext _dbContext;
        private readonly IGenericRepository<Client> _service;

        public ClientsController(IGenericRepository<Client> service, Sale_ManagementDbContext dbContext)
        {
            _service = service;
            _dbContext = dbContext;
        }
        // get All clients
        [HttpGet("GetClients")]
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
        [HttpPost("Create")]
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
        public async Task<IActionResult> UpdateClient(int id,[FromBody]ClientDto newClient)
        {
            //find article
            var _existingClient = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
            // check article existance
            if (_existingClient==null)
            {
                return NotFound();
            }
            _existingClient.FName = newClient.FName;
            _existingClient.LName = newClient.LName;
            _existingClient.Email = newClient.Email;
            _existingClient.PhoneNumber = newClient.PhoneNumber;

            _dbContext.Entry(_existingClient).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok(_existingClient);
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
