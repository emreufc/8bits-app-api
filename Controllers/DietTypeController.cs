using _8bits_app_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _8_bits.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietTypeController : ControllerBase
    {
        private readonly mydbcontext _context;

        public DietTypeController(mydbcontext context)
        {
            _context = context;
        }

        // GET: api/DietType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DietType>>> GetDietType()
        {
            var dietTypes = await _context.DietTypes.ToListAsync();
            if (dietTypes == null || dietTypes.Count == 0)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "No diet types found in the database. Please ensure that diet types have been added before querying.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {dietTypes.Count()} diet types from the database.",
                data = dietTypes
            });
        }

        // GET: api/DietType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DietType>> GetDietType(int id)
        {
            var dietType = await _context.DietTypes.FindAsync(id);

            if (dietType == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No diet type found with ID {id}. Please check the ID and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Diet type with ID {id} retrieved successfully.",
                data = dietType
            });
        }
    }
}
