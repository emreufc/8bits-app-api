using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace _8bits_app_api.Models;

public partial class RecipeStep
{
    public int? RecipeId { get; set; }

    public string? RecipeName { get; set; }

    public byte? StepNum { get; set; }

    public string? Step { get; set; }

    public int RecipeStepsId { get; set; }

    public bool? IsDeleted { get; set; }

    [JsonIgnore]
    public virtual Recipe? Recipe { get; set; }
}
