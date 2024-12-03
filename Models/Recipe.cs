using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string RecipeName { get; set; } = null!;

    public string Serving { get; set; } = null!;

    public string PreparationTime { get; set; } = null!;

    public string CookTime { get; set; } = null!;

    public string Calorie { get; set; } = null!;

    public string Category { get; set; } = null!;

    public double Protein { get; set; }

    public double Carbohydrates { get; set; }

    public double Fat { get; set; }

    public virtual ICollection<FavoriteRecipe> FavoriteRecipes { get; set; } = new List<FavoriteRecipe>();

    public virtual RecipeImage? RecipeImage { get; set; }

    public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();
}
