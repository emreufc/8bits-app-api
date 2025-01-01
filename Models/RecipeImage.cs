using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class RecipeImage
{
    public int RecipeImageId { get; set; }

    public int RecipeId { get; set; }

    public string ImagePath { get; set; } = null!;
}
