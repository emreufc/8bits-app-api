using _8bits_app_api.Models;

namespace _8bits_app_api.Repositories
{
    public interface IRecipeIngredientRepository
    {
        Task<IEnumerable<RecipeIngredient>> GetAllRecipeIngredientAsync();
        Task<RecipeIngredient> GetRecipeIngredientByIdAsync(int id);
        Task<IEnumerable<RecipeIngredient>> GetRecipeIngredientsByRecipeIdAsync(int recipeId);

    }
}