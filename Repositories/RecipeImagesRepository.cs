using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class RecipeImagesRepository : IRecipeImagesRepository
    {
        private readonly mydbcontext _mydbcontext;

        public RecipeImagesRepository(mydbcontext mydbcontext)
        {
            _mydbcontext = mydbcontext;
        }
        public async Task<IEnumerable<RecipeImage>> GetAllRecipeImagesAsync()
        {
            return await _mydbcontext.RecipeImages.ToListAsync();
        }
        public async Task<RecipeImage> GetRecipeImageById(int id)
        {
            return await _mydbcontext.RecipeImages.FindAsync(id);
        }
    }
}
