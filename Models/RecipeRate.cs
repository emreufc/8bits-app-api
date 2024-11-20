using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class RecipeRate
{
    public short? RecipeId { get; set; }

    public string? RecipeName { get; set; }

    public double? RecipeRate1 { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
