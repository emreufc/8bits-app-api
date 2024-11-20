using _8bits_app_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _8_bits.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _8bitsDbController : ControllerBase
    {
        private readonly MyDbContext _context;

        public _8bitsDbController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            var recipes = await _context.Recipes.ToListAsync();
            if (recipes == null || recipes.Count == 0)
            {
                return NotFound("No recipes found.");
            }

            return Ok(recipes);
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound($"Recipe with ID {id} not found.");
            }

            return Ok(recipe);
        }
    }
}
