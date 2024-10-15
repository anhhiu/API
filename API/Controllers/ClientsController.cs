using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbcontext context;

        public ClientsController(ApplicationDbcontext context)
        {
           this.context = context;
        }

        [HttpGet]
        public  List<Client> GetClients()
        {
            return context.Clients.OrderByDescending(c => c.Id).ToList();
        }


        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            var client = context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]

        public IActionResult CreateClient(ClientDto clientDto)
        {
            var otherClient = context.Clients.FirstOrDefault(c => c.Email == clientDto.Email);
            if (otherClient != null)
            {
                ModelState.AddModelError("Email","Email đã được sử dụng");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var client = new Client()
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
                Status = clientDto.Status,
                CreatedAt = DateTime.Now,
            };
            context.Clients.Add(client);
            context.SaveChanges();
            return Ok(client);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateClient(int id, ClientDto clientDto)
        {
            var ortherChient = context.Clients.FirstOrDefault(c => c.Email == clientDto.Email);

            if (ortherChient != null)
            {
                ModelState.AddModelError("Email", "Email đã tồn tại");
                var validation =new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var client = context.Clients.Find(id);

            if (client == null)
            {
                return NotFound();
            }

            client.FirstName = clientDto.FirstName;
            client.LastName = clientDto.LastName;
            client.Email = clientDto.Email;
            client.Phone = clientDto.Phone ?? "";
            client.Address = clientDto.Address ?? "";
            client.Status = clientDto.Status;

            context.SaveChanges();
            return Ok(client);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteClient(int id)
        {

            var client = context.Clients.Find(id);

            if(client == null)
            {
                return NotFound();
            }

            context.Clients.Remove(client);
            context.SaveChanges();

            return Ok("Xóa thành công");
        }

    }
}
