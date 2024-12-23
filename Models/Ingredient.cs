using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public string IngredientName { get; set; } = null!;

    public string? IngImgUrl { get; set; }

    public int? AllergenId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Allergen? Allergen { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    public virtual ICollection<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();

    public virtual ICollection<UserInventory> UserInventories { get; set; } = new List<UserInventory>();
}
