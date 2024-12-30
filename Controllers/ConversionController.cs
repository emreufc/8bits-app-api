using _8bits_app_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _8bits_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : ControllerBase
    {
        private readonly IConversionService _conversionService;

        public ConversionController(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        // GET: api/IngredientConversions/{ingredientId}
        [HttpGet("{ingredientId}")]
        public async Task<IActionResult> GetConversionsByIngredientId(int ingredientId)
        {
            if (ingredientId <= 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Invalid ingredient ID. Please provide a valid ID.",
                    data = (object)null
                });
            }

            var conversions = await _conversionService.GetConversionsByIngredientIdAsync(ingredientId);

            if (conversions == null || !conversions.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No conversions found for ingredient ID {ingredientId}.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved conversions for ingredient ID {ingredientId}.",
                data = conversions
            });
        }
    }

}
