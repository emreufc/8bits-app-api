using _8bits_app_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _8_bits.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergenController : ControllerBase
    {
        private readonly IAllergenReadingService _allergenReadingService;

        public AllergenController(IAllergenReadingService allergenReadingService)
        {
            _allergenReadingService = allergenReadingService;
        }

        // GET: api/Allergen
        [HttpGet]
        public async Task<IActionResult> GetAllergens([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Page number and page size must be greater than 0.",
                    data = (object)null
                });
            }

            var (allergens, totalCount) = await _allergenReadingService.GetAllergensPaginatedAsync(pageNumber, pageSize);
            if (allergens == null || !allergens.Any())
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
                message = $"Successfully retrieved {allergens.Count()} allergies from the database.",
                data = allergens,
                pagination = new
                {
                    currentPage = pageNumber,
                    pageSize,
                    totalRecords = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

        // GET: api/Allergen/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllergen(int id)
        {
            var allergen = await _allergenReadingService.GetAllergenByIdAsync(id);
            if (allergen == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No allergen found with ID {id}. Please check the ID and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Allergen with ID {id} retrieved successfully.",
                data = allergen
            });
        }
    }
}
