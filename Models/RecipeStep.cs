using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class RecipeStep
{
    public int RecipeId { get; set; }

    public string RecipeName { get; set; } = null!;

    public int StepNum { get; set; }

    public string Step { get; set; } = null!;

    public int RecipestepsId { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;
}
