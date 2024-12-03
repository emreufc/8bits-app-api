using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class RecipeIngredient
{
    public int RecipeId { get; set; }

    public int IngredientId { get; set; }

    public string IngredientName { get; set; } = null!;

    public string Quantity { get; set; } = null!;

    public string QuantityType { get; set; } = null!;

    public int RecipeingredientId { get; set; }
}
