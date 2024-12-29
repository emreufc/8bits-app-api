using System.Text.Json.Serialization;

namespace _8bits_app_api.Dtos;

public class FavoriteRecipeDto
{
    [JsonIgnore]
    public int UserId { get; set; }
    public int RecipeId { get; set; }
    [JsonIgnore]
    public bool IsDeleted { get; set; }
}
