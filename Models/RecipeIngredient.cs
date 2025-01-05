using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace _8bits_app_api.Models;

public partial class RecipeIngredient
{
    public int RecipeId { get; set; }

    public int IngredientId { get; set; }

    public double Quantity { get; set; }

    public int QuantityTypeId { get; set; }

    public int RecipeIngredientId { get; set; }

    public bool? IsDeleted { get; set; }

    [JsonIgnore]
    public virtual Ingredient Ingredient { get; set; } = null!;
    [JsonIgnore]
    public virtual QuantityType QuantityType { get; set; } = null!;
    [JsonIgnore]
    public virtual Recipe Recipe { get; set; } = null!;
}
