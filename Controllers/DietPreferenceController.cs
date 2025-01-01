using _8bits_app_api.Controllers;
using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace _8_bits.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietPreferenceController : BaseController
    {
        private readonly IDietPreferenceReadingService _dietPreferenceReadingService;

        public DietPreferenceController(IDietPreferenceReadingService dietPreferenceReadingService)
        {
            _dietPreferenceReadingService = dietPreferenceReadingService;
        }

        // PUT: api/DietPreferences/update
        [HttpPost("updatePreferences")]
        public async Task<IActionResult> UpdateDietPreferences([FromBody] List<int> dietPreferenceIds)
        {
            if (dietPreferenceIds == null || !dietPreferenceIds.Any())
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Diet preference list cannot be null or empty.",
                    data = (object)null
                });
            }

            var userId = GetCurrentUserId();

            try
            {
                await _dietPreferenceReadingService.UpdateDietPreferencesAsync(userId, dietPreferenceIds);

                return Ok(new
                {
                    code = 200,
                    message = "Diet preferences updated successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    code = 500,
                    message = ex.Message,
                    data = (object)null
                });
            }
        }

        // GET: api/DietPreference
        [HttpGet]
        public async Task<IActionResult> GetDietPreferences([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
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

            var (dietPreferences, totalCount) = await _dietPreferenceReadingService.GetDietPreferencesPaginatedAsync(pageNumber, pageSize);
            if (dietPreferences == null || !dietPreferences.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = "No diet preferences found in the database. Please ensure that diet preferences have been added before querying.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {dietPreferences.Count()} diet preferences from the database.",
                data = dietPreferences,
                pagination = new
                {
                    currentPage = pageNumber,
                    pageSize,
                    totalRecords = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }
        [HttpGet("getByCurrentUser")]
        [HttpGet]
        public async Task<IActionResult> GetDietPreferencesByUser([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
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

            int userId = this.GetCurrentUserId();
            var (dietPreferences, totalCount) = await _dietPreferenceReadingService.GetDietPreferencesByUserAsync(pageNumber, pageSize, userId);
            if (dietPreferences == null || !dietPreferences.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = "No diet preferences found in the database. Please ensure that diet preferences have been added before querying.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {dietPreferences.Count()} diet preferences from the database.",
                data = dietPreferences,
                pagination = new
                {
                    currentPage = pageNumber,
                    pageSize,
                    totalRecords = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

        // GET: api/DietPreference/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDietPreference(int id)
        {
            var dietPreference = await _dietPreferenceReadingService.GetDietPreferenceByIdAsync(id);
            if (dietPreference == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No diet preference found with ID {id}. Please check the ID and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Diet preference with ID {id} retrieved successfully.",
                data = dietPreference
            });
        }
    }
}
