using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class RecipeRate
{
    public int RecipeId { get; set; }

    public string RecipeName { get; set; } = null!;

    public double RecipeRate1 { get; set; }
}
