using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class IngredientConversion
{
    public int IngredientId { get; set; }

    public string IngredientName { get; set; } = null!;

    public int QuantityTypeId { get; set; }

    public string QuantityTypeDesc { get; set; } = null!;

    public double? ConversionToMl { get; set; }

    public double? ConversionToGrams { get; set; }
}
