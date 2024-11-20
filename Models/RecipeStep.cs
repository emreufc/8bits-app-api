using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class RecipeStep
{
    public short? RecipeId { get; set; }

    public string RecipeName { get; set; } = null!;

    public string StepNum { get; set; } = null!;

    public string Step { get; set; } = null!;

    public virtual Recipe? Recipe { get; set; }
}
