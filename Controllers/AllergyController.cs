using _8bits_app_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _8_bits.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergyController : ControllerBase
    {
        private readonly mydbcontext _context;

        public AllergyController(mydbcontext context)
        {
            _context = context;
        }

        // GET: api/Allergy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Allergy>>> GetAllergy()
        {
            var allergies = await _context.Allergies.ToListAsync();
            if (allergies == null || allergies.Count == 0)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "No allergies found in the database. Please ensure that allergies have been added before querying.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {allergies.Count()} allergies from the database.",
                data = allergies
            });
        }

        // GET: api/Allergy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Allergy>> GetAllergy(int id)
        {
            var allergy = await _context.Allergies.FindAsync(id);

            if (allergy == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No allergy found with ID {id}. Please check the ID and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Allergy with ID {id} retrieved successfully.",
                data = allergy
            });
        }
    }
}
