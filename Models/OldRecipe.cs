using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class OldRecipe
{
    public int OldRecipeId { get; set; }

    public int RecipeId { get; set; }

    public int UserId { get; set; }

    public DateTime? AddedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
