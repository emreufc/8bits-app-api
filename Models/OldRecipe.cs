using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class OldRecipe
{
    public int OldRecipeId { get; set; }

    public int RecipeId { get; set; }

    public int UserId { get; set; }

    public DateTime? AddedDate { get; set; }
}
