using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string? RecipeName { get; set; }

    public byte? PersonCount { get; set; }

    public byte? PreparationTime { get; set; }

    public byte? CookingTime { get; set; }

    public short? GramPerServing { get; set; }

    public short? KcalPerServing { get; set; }

    public string? ImageUrl { get; set; }

    public double? Carbohydrate { get; set; }

    public double? Protein { get; set; }

    public double? Fat { get; set; }

    public double RecipeRate { get; set; }

    public bool? Vegan { get; set; }

    public bool? Vegetarian { get; set; }

    public bool? Pescatarian { get; set; }

    public bool? Keto { get; set; }

    public bool? Paleo { get; set; }

    public bool? Mediterranean { get; set; }

    public bool? GlutenFree { get; set; }

    public bool? DairyFree { get; set; }

    public bool? LowCarb { get; set; }

    public bool? Flexitarian { get; set; }

    public bool? Normal { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<FavoriteRecipe> FavoriteRecipes { get; set; } = new List<FavoriteRecipe>();

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();
}
