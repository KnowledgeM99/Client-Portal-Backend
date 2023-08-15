
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
    public class PolicyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PolicyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Policy>> GetPolicies()
        {
            return _context.Policies.Include(p => p.Client).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Policy> GetPolicyById(string id)
        {
            var policy = _context.Policies.Include(p => p.Client).FirstOrDefault(p => p.PolicyNumber == id);
            if (policy == null)
            {
                return NotFound();
            }
            return Ok(policy);
        }

        [HttpPost]
        public IActionResult CreatePolicy(Policy policy)
        {
            // Ensure that the ClientId property is set and valid
            var client = _context.Clients.Find(policy.ClientId);
            if (client == null)
            {
                return BadRequest("Invalid client specified.");
            }

            policy.Client = null; // Remove circular reference
            _context.Policies.Add(policy);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPolicyById), new { id = policy.PolicyNumber }, policy);
        }



        [HttpDelete("{id}")]
        public IActionResult DeletePolicy(string id)
        {
            var policy = _context.Policies.Find(id);
            if (policy == null)
            {
                return NotFound();
            }
            _context.Policies.Remove(policy);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
