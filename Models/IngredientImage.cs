using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class IngredientImage
{
    public int IngredientId { get; set; }

    public string IngredientName { get; set; } = null!;

    public string IngredientNameEn { get; set; } = null!;

    public string? PageUrl { get; set; }
}
