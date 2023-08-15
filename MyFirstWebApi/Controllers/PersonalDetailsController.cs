using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebApi.Models;

namespace MyFirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalDetailsController : ControllerBase
    {
        private static List<PersonalDetailsModel> personalDetails = new List<PersonalDetailsModel>()
        {
            new PersonalDetailsModel { Id = 1, Age = 25, Name = "John Doe" },
            new PersonalDetailsModel { Id = 2, Age = 30, Name = "Jane Smith" },
            new PersonalDetailsModel { Id = 3, Age = 24, Name = "Knowledge" },
            new PersonalDetailsModel { Id = 4, Age = 19, Name = "Matimba"}
            
        };


        [HttpGet]
        public ActionResult<IEnumerable<PersonalDetailsModel>> GetAllPersonalDetails()
        {
            return Ok(personalDetails);
        }

        [HttpGet("{id}")]
        public ActionResult<PersonalDetailsModel> GetPersonalDetails(int id)
        {
            var details = personalDetails.Find(d => d.Id == id);
            if (details == null)
            {
                return NotFound();
            }
            return Ok(details);
        }

        [HttpPost]
        public ActionResult<PersonalDetailsModel> AddPersonalDetails(PersonalDetailsModel details)
        {
            details.Id = GenerateNewId();
            personalDetails.Add(details);
            return CreatedAtAction(nameof(GetPersonalDetails), new { id = details.Id }, details);
        }

        [HttpPut("{id}")]
        public ActionResult<PersonalDetailsModel> UpdatePersonalDetails(int id, PersonalDetailsModel updatedDetails)
        {
            var existingDetails = personalDetails.Find(d => d.Id == id);
            if (existingDetails == null)
            {
                return NotFound();
            }

            existingDetails.Name = updatedDetails.Name;
            existingDetails.Age = updatedDetails.Age;

            return Ok(existingDetails);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePersonalDetails(int id)
        {
            var existingDetails = personalDetails.Find(d => d.Id == id);
            if (existingDetails == null)
            {
                return NotFound();
            }

            personalDetails.Remove(existingDetails);
            return NoContent();
        }

        private int GenerateNewId()
        {
            var random = new Random();
            int newId;
            do
            {
                newId = random.Next(1, 1000);
            } while (personalDetails.Exists(d => d.Id == newId));
            return newId;
        }
    }
}

