using _8bits_app_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _8_bits.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietTypeController : ControllerBase
    {
        private readonly IDietTypeReadingService _dietTypeReadingService;

        public DietTypeController(IDietTypeReadingService dietTypeReadingService)
        {
            _dietTypeReadingService = dietTypeReadingService;
        }

        // GET: api/DietType
        [HttpGet]
        public async Task<IActionResult> GetDietTypes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
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

            var (dietTypes, totalCount) = await _dietTypeReadingService.GetDietTypesPaginatedAsync(pageNumber, pageSize);
            if (dietTypes == null || !dietTypes.Any())
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
                data = dietTypes,
                pagination = new
                {
                    currentPage = pageNumber,
                    pageSize,
                    totalRecords = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

        // GET: api/DietType/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDietType(int id)
        {
            var dietType = await _dietTypeReadingService.GetDietTypeByIdAsync(id);
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
