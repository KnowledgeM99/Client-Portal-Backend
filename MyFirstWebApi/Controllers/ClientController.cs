

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients()
        {
            return _context.Clients.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetClientById(string id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]
        public IActionResult CreateClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetClientById), new { id = client.IdNumber }, client);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(string id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            _context.Clients.Remove(client);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
