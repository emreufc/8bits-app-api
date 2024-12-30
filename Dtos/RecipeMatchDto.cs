namespace _8bits_app_api.Dtos
{
    public class RecipeWithMatchDto
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; } = null!;
        public double MatchPercentage { get; set; }
    }
}
