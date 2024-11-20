using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public string IngredientName { get; set; } = null!;

    public string? PageUrl { get; set; }

    public string DetailedAllergenInfoTr { get; set; } = null!;

    public virtual ICollection<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();

    public virtual ICollection<UserInventory> UserInventories { get; set; } = new List<UserInventory>();
}
