using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class FavoriteRecipe
{
    public int FavId { get; set; }

    public int? UserId { get; set; }

    public int? RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }
}
